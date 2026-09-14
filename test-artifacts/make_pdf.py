import sys

def build_pdf(text_lines, out_path):
    objects = []

    objects.append("<< /Type /Catalog /Pages 2 0 R >>")
    objects.append("<< /Type /Pages /Kids [3 0 R] /Count 1 >>")
    objects.append("<< /Type /Page /Parent 2 0 R /Resources << /Font << /F1 4 0 R >> >> /MediaBox [0 0 612 792] /Contents 5 0 R >>")
    objects.append("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>")

    content_lines = []
    y = 720
    for line in text_lines:
        escaped = line.replace("\\", r"\\").replace("(", r"\(").replace(")", r"\)")
        content_lines.append(f"BT /F1 12 Tf 72 {y} Td ({escaped}) Tj ET")
        y -= 18
    content_stream = "\n".join(content_lines).encode("latin-1")
    objects.append(f"<< /Length {len(content_stream)} >>\nstream\n" + content_stream.decode("latin-1") + "\nendstream")

    pdf = bytearray()
    pdf += b"%PDF-1.4\n"
    offsets = [0]
    for i, obj in enumerate(objects, start=1):
        offsets.append(len(pdf))
        pdf += f"{i} 0 obj\n".encode("latin-1")
        pdf += obj.encode("latin-1")
        pdf += b"\nendobj\n"

    xref_offset = len(pdf)
    n = len(objects) + 1
    pdf += f"xref\n0 {n}\n".encode("latin-1")
    pdf += b"0000000000 65535 f \n"
    for off in offsets[1:]:
        pdf += f"{off:010} 00000 n \n".encode("latin-1")

    pdf += f"trailer\n<< /Size {n} /Root 1 0 R >>\nstartxref\n{xref_offset}\n%%EOF".encode("latin-1")

    with open(out_path, "wb") as f:
        f.write(pdf)

if __name__ == "__main__":
    lines = [
        "Zamba.Api Test Document",
        "This PDF was generated to test the /api/copilot/ask endpoint.",
        "Zamba is an HR and document management platform.",
        "The volume manager compresses legajo (employee file) documents.",
        "This endpoint should extract this text, chunk it, embed it,",
        "and use it as context to answer prompts about this document.",
    ]
    build_pdf(lines, sys.argv[1] if len(sys.argv) > 1 else "test.pdf")
    print("PDF written")
