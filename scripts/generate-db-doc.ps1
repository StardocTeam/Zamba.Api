# Generates docs/DATABASE.md from the exported schema CSV files (schema_columns.csv, schema_pks.csv, schema_fks.csv, schema_rowcounts.csv)
param(
    [string]$Root = (Join-Path $PSScriptRoot ".."),
    [string]$OutFile = (Join-Path $Root "docs\DATABASE.md")
)

function Read-PipeCsv($path) {
    $lines = Get-Content -Path $path | Select-Object -Skip 0
    # first line = headers, second line = dashes separator (sqlcmd -W header underline), rest = data
    $header = $lines[0] -split '\|'
    $rows = @()
    for ($i = 2; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        if ([string]::IsNullOrWhiteSpace($line)) { continue }
        $parts = $line -split '\|'
        $obj = [ordered]@{}
        for ($j = 0; $j -lt $header.Count; $j++) {
            $obj[$header[$j].Trim()] = if ($j -lt $parts.Count) { $parts[$j].Trim() } else { "" }
        }
        $rows += [pscustomobject]$obj
    }
    return $rows
}

$columns = Read-PipeCsv (Join-Path $Root "schema_columns.csv")
$pks = Read-PipeCsv (Join-Path $Root "schema_pks.csv")
$fks = Read-PipeCsv (Join-Path $Root "schema_fks.csv")
$rowcounts = Read-PipeCsv (Join-Path $Root "schema_rowcounts.csv")

$pkLookup = @{}
foreach ($pk in $pks) {
    $key = "$($pk.TABLE_SCHEMA).$($pk.TABLE_NAME)"
    if (-not $pkLookup.ContainsKey($key)) { $pkLookup[$key] = @() }
    $pkLookup[$key] += $pk.COLUMN_NAME
}

$fkByTable = @{}
foreach ($fk in $fks) {
    $key = "$($fk.parent_schema).$($fk.parent_table)"
    if (-not $fkByTable.ContainsKey($key)) { $fkByTable[$key] = @() }
    $fkByTable[$key] += $fk
}

$rowCountLookup = @{}
foreach ($rc in $rowcounts) {
    $key = "$($rc.table_schema).$($rc.table_name)"
    $rowCountLookup[$key] = $rc.row_count
}

$grouped = $columns | Group-Object TABLE_SCHEMA, TABLE_NAME

New-Item -ItemType Directory -Path (Split-Path $OutFile) -Force | Out-Null

$sb = New-Object System.Text.StringBuilder
[void]$sb.AppendLine("# Zamba RRHH Database Documentation")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("Generated from SQL Server database `zambabpm_RRHH`. Fill in the **Description** column/section for each table and column.")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("## Table of Contents")
[void]$sb.AppendLine("")

$tableGroups = $grouped | Sort-Object { $_.Group[0].TABLE_SCHEMA }, { $_.Group[0].TABLE_NAME }

foreach ($g in $tableGroups) {
    $schema = $g.Group[0].TABLE_SCHEMA
    $table = $g.Group[0].TABLE_NAME
    $anchor = ("$schema-$table" -replace '[^a-zA-Z0-9\-]', '').ToLower()
    [void]$sb.AppendLine("- [$schema.$table](#$anchor)")
}

[void]$sb.AppendLine("")
[void]$sb.AppendLine("## Relaciones entre tablas")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("Relaciones declaradas mediante claves foraneas en SQL Server. La direccion es **tabla hija.columna -> tabla padre.columna**.")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("| Tabla hija | Columna hija | Tabla padre | Columna padre | Restriccion |")
[void]$sb.AppendLine("|------------|--------------|-------------|---------------|-------------|")
foreach ($fk in ($fks | Sort-Object parent_schema, parent_table, constraint_name, parent_column)) {
    [void]$sb.AppendLine("| $($fk.parent_schema).$($fk.parent_table) | $($fk.parent_column) | $($fk.ref_schema).$($fk.ref_table) | $($fk.ref_column) | $($fk.constraint_name) |")
}
[void]$sb.AppendLine("")
[void]$sb.AppendLine("---")
[void]$sb.AppendLine("")

foreach ($g in $tableGroups) {
    $schema = $g.Group[0].TABLE_SCHEMA
    $table = $g.Group[0].TABLE_NAME
    $key = "$schema.$table"
    $rowCount = if ($rowCountLookup.ContainsKey($key)) { $rowCountLookup[$key] } else { "?" }
    $pkCols = if ($pkLookup.ContainsKey($key)) { $pkLookup[$key] } else { @() }

    [void]$sb.AppendLine("## $schema.$table")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("**Row count:** $rowCount")
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("**Description:** _TODO_")
    [void]$sb.AppendLine("")

    if ($fkByTable.ContainsKey($key)) {
        [void]$sb.AppendLine("**Foreign keys:**")
        foreach ($fk in $fkByTable[$key]) {
            [void]$sb.AppendLine("- $($fk.parent_column) -> $($fk.ref_schema).$($fk.ref_table).$($fk.ref_column) (`$($fk.constraint_name)`)")
        }
        [void]$sb.AppendLine("")
    }

    [void]$sb.AppendLine("| Column | Type | Nullable | PK | Description |")
    [void]$sb.AppendLine("|--------|------|----------|----|--------------|")

    $sortedCols = $g.Group | Sort-Object { [int]$_.ORDINAL_POSITION }
    foreach ($c in $sortedCols) {
        $type = $c.DATA_TYPE
        if ($c.MAXLEN) { $type += "($($c.MAXLEN))" }
        elseif ($c.PREC -and $c.DATA_TYPE -in @('numeric','decimal')) { $type += "($($c.PREC),$($c.SCALE))" }
        $isPk = if ($pkCols -contains $c.COLUMN_NAME) { "PK" } else { "" }
        [void]$sb.AppendLine("| $($c.COLUMN_NAME) | $type | $($c.IS_NULLABLE) | $isPk | |")
    }
    [void]$sb.AppendLine("")
    [void]$sb.AppendLine("---")
    [void]$sb.AppendLine("")
}

Set-Content -Path $OutFile -Value $sb.ToString() -Encoding UTF8
Write-Output "Generated $OutFile with $($tableGroups.Count) tables."
