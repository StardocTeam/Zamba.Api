using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using Zamba.Api.Data;

namespace Zamba.Api.Tests;

[TestClass]
public sealed class DatabaseSqlDialectTests
{
    [TestMethod]
    public void SqlServerDialect_UsesSqlServerSyntax()
    {
        var dialect = DatabaseSqlDialect.FromProviderName("Microsoft.EntityFrameworkCore.SqlServer");

        Assert.AreEqual("[WFDocument]", dialect.QuoteIdentifier("WFDocument"));
        Assert.AreEqual("[userId]", dialect.QuoteAlias("userId"));
        Assert.AreEqual("@userId", dialect.Parameter("userId"));
        Assert.AreEqual("@userId", dialect.CommandParameterName("userId"));
        Assert.AreEqual("SELECT TOP (@limit) ", dialect.SelectWithLimit("limit"));
        Assert.AreEqual(string.Empty, dialect.FetchFirst("limit"));
        Assert.AreEqual("SELECT [Id] FROM [Users] WHERE Id = @id;", dialect.PrepareCommandText("SELECT [Id] FROM [Users] WHERE Id = @id;"));
        Assert.IsTrue(dialect.ColumnsQuery.Contains("INFORMATION_SCHEMA.COLUMNS", StringComparison.Ordinal));
    }

    [TestMethod]
    public void OracleDialect_UsesOracleSyntax()
    {
        var dialect = DatabaseSqlDialect.FromProviderName("Oracle.EntityFrameworkCore");

        Assert.AreEqual("\"WFDocument\"", dialect.QuoteIdentifier("WFDocument"));
        Assert.AreEqual("\"userId\"", dialect.QuoteAlias("userId"));
        Assert.AreEqual(":userId", dialect.Parameter("userId"));
        Assert.AreEqual("userId", dialect.CommandParameterName("userId"));
        Assert.AreEqual("SELECT ", dialect.SelectWithLimit("limit"));
        Assert.AreEqual(" FETCH FIRST :limit ROWS ONLY", dialect.FetchFirst("limit"));
        Assert.AreEqual("SELECT \"ID\" FROM \"USERS\" WHERE Id = :id", dialect.PrepareCommandText("SELECT [Id] FROM [Users] WHERE Id = @id;"));
        Assert.AreEqual("SELECT \"ID\" AS \"userId\" FROM \"USERS\"", dialect.PrepareCommandText("SELECT [Id] AS [userId] FROM [Users];"));
        Assert.IsTrue(dialect.ColumnsQuery.Contains("ALL_TAB_COLUMNS", StringComparison.Ordinal));
    }

    [TestMethod]
    public void FromProviderName_RejectsUnsupportedProvider()
    {
        Assert.ThrowsException<InvalidOperationException>(
            () => DatabaseSqlDialect.FromProviderName("Microsoft.EntityFrameworkCore.Sqlite"));
    }

    [TestMethod]
    public void FromConnection_RecognizesSqlServerAndOracleConnections()
    {
        Assert.IsFalse(DatabaseSqlDialect.FromConnection(new SqlConnection()).IsOracle);
        Assert.IsTrue(DatabaseSqlDialect.FromConnection(new OracleConnection()).IsOracle);
    }
}