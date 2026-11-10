using Pt = (double X, double Y);
using Rgb = (double R, double G, double B);
 
public static class FaviconGenerator
{
    private const double Design = 128.0;
    private static readonly int[] Sizes = { 16, 32, 48, 64, 128 };
 
    private static readonly Rgb Start    = (179, 140, 251);
    private static readonly Rgb Middle   = (126, 147, 252);
    private static readonly Rgb End      = (70, 166, 255);
    private static readonly Rgb White    = (255, 255, 255);
    private static readonly Rgb Lavender = (196, 181, 253);
    private static readonly Rgb SkyBlue  = (147, 197, 253);
 
    private static readonly List<Pt> LeftPage  = BuildPage(mirror: false);
    private static readonly List<Pt> RightPage = BuildPage(mirror: true);
    private static readonly List<Pt> BigStar   = BuildStar(92, 32, 15);
    private static readonly List<Pt> SmallStar = BuildStar(36, 38, 7);
    private static readonly List<(List<Pt> Line, Rgb Color)> TextLines = BuildTextLines();
 
    public static void Generate(string path)
    {
        var images = new List<(int Size, byte[] Data)>();
        foreach (int size in Sizes)
            images.Add((size, BuildDib(size, Render(size))));
 
        using var ms = new MemoryStream();
        using var w = new BinaryWriter(ms);
 
        w.Write((short)0);
        w.Write((short)1);
        w.Write((short)images.Count);
 
        int offset = 6 + 16 * images.Count;
        foreach (var (size, data) in images)
        {
            w.Write((byte)(size >= 256 ? 0 : size));
            w.Write((byte)(size >= 256 ? 0 : size));
            w.Write((byte)0);
            w.Write((byte)0);
            w.Write((short)1);
            w.Write((short)32);
            w.Write(data.Length);
            w.Write(offset);
            offset += data.Length;
        }
 
        foreach (var (_, data) in images)
            w.Write(data);
        w.Flush();
 
        string fullPath = Path.GetFullPath(path);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        File.WriteAllBytes(fullPath, ms.ToArray());
    }
 
    private static byte[] BuildDib(int size, byte[] bgraTopDown)
    {
        using var ms = new MemoryStream();
        using var w = new BinaryWriter(ms);
 
        w.Write(40);
        w.Write(size);
        w.Write(size * 2);
        w.Write((short)1);
        w.Write((short)32);
        w.Write(0);
        w.Write(size * size * 4);
        w.Write(0); w.Write(0);
        w.Write(0); w.Write(0);
 
        for (int y = size - 1; y >= 0; y--)
            w.Write(bgraTopDown, y * size * 4, size * 4);
 
        int maskRow = (size + 31) / 32 * 4;
        w.Write(new byte[maskRow * size]);
 
        w.Flush();
        return ms.ToArray();
    }
 
    private static byte[] Render(int size)
    {
        int ss = size <= 32 ? 8 : 4;
        double step = Design / size;
        var bgra = new byte[size * size * 4];
 
        for (int py = 0; py < size; py++)
        {
            for (int px = 0; px < size; px++)
            {
                double sumA = 0, sumR = 0, sumG = 0, sumB = 0;
 
                for (int sy = 0; sy < ss; sy++)
                {
                    for (int sx = 0; sx < ss; sx++)
                    {
                        double x = (px + (sx + 0.5) / ss) * step;
                        double y = (py + (sy + 0.5) / ss) * step;
                        var s = Shade(x, y);
                        sumA += s.A;
                        sumR += s.R * s.A;
                        sumG += s.G * s.A;
                        sumB += s.B * s.A;
                    }
                }
 
                if (sumA <= 0) continue;
 
                int i = (py * size + px) * 4;
                bgra[i]     = ToByte(sumB / sumA);
                bgra[i + 1] = ToByte(sumG / sumA);
                bgra[i + 2] = ToByte(sumR / sumA);
                bgra[i + 3] = ToByte(255.0 * sumA / (ss * ss));
            }
        }
        return bgra;
    }
 
    private static (double R, double G, double B, double A) Shade(double x, double y)
    {
        if (!InRoundedRect(x, y)) return (0, 0, 0, 0);
 
        Rgb c = Gradient((x + y) / (2 * Design));
 
        double ex = (x - 44) / 48.0, ey = (y - 20) / 20.0;
        if (ex * ex + ey * ey <= 1) c = Mix(c, White, 0.16);
 
        if (InPolygon(LeftPage, x, y)) c = White;
        else if (InPolygon(RightPage, x, y)) c = Mix(c, White, 0.88);
 
        if (x >= 62.75 && x <= 65.25 && y >= 54 && y <= 97) c = Lavender;
 
        foreach (var (line, color) in TextLines)
            if (DistanceToPolyline(line, x, y) <= 1.5) c = color;
 
        if (InPolygon(BigStar, x, y)) c = White;
        else if (InPolygon(SmallStar, x, y)) c = Mix(c, White, 0.85);
 
        return (c.R, c.G, c.B, 1.0);
    }
 
    private static List<Pt> BuildPage(bool mirror)
    {
        var pts = new List<Pt>();
        AddCubic(pts, (64, 54), (54, 47), (40, 46), (28, 50));
        pts.Add((28, 94));
        AddCubic(pts, (28, 94), (40, 90), (54, 91), (64, 97));
 
        if (mirror)
            for (int i = 0; i < pts.Count; i++)
                pts[i] = (Design - pts[i].X, pts[i].Y);
 
        return pts;
    }
 
    private static List<Pt> BuildStar(double cx, double cy, double r)
    {
        double k = 0.3 * r;
        var pts = new List<Pt>();
        AddQuad(pts, (cx, cy - r), (cx + k, cy - k), (cx + r, cy));
        AddQuad(pts, (cx + r, cy), (cx + k, cy + k), (cx, cy + r));
        AddQuad(pts, (cx, cy + r), (cx - k, cy + k), (cx - r, cy));
        AddQuad(pts, (cx - r, cy), (cx - k, cy - k), (cx, cy - r));
        return pts;
    }
 
    private static List<(List<Pt> Line, Rgb Color)> BuildTextLines()
    {
        var list = new List<(List<Pt> Line, Rgb Color)>();
 
        foreach (double y in new[] { 61.0, 70.0, 79.0 })
        {
            var l = new List<Pt>();
            AddCubic(l, (37, y), (43, y - 2), (50, y - 2), (56, y));
            list.Add((l, Lavender));
        }
        foreach (double y in new[] { 61.0, 70.0 })
        {
            var l = new List<Pt>();
            AddCubic(l, (72, y), (78, y - 2), (85, y - 2), (91, y));
            list.Add((l, SkyBlue));
        }
        return list;
    }
 
    private static void AddCubic(List<Pt> pts, Pt p0, Pt p1, Pt p2, Pt p3, int steps = 24)
    {
        for (int i = 0; i <= steps; i++)
        {
            double t = (double)i / steps, u = 1 - t;
            pts.Add((
                u * u * u * p0.X + 3 * u * u * t * p1.X + 3 * u * t * t * p2.X + t * t * t * p3.X,
                u * u * u * p0.Y + 3 * u * u * t * p1.Y + 3 * u * t * t * p2.Y + t * t * t * p3.Y));
        }
    }
 
    private static void AddQuad(List<Pt> pts, Pt p0, Pt p1, Pt p2, int steps = 16)
    {
        for (int i = 0; i <= steps; i++)
        {
            double t = (double)i / steps, u = 1 - t;
            pts.Add((
                u * u * p0.X + 2 * u * t * p1.X + t * t * p2.X,
                u * u * p0.Y + 2 * u * t * p1.Y + t * t * p2.Y));
        }
    }
 
    private static bool InRoundedRect(double x, double y)
    {
        if (x < 0 || x > Design || y < 0 || y > Design) return false;
        const double radius = 32;
        double cx = Math.Clamp(x, radius, Design - radius);
        double cy = Math.Clamp(y, radius, Design - radius);
        double dx = x - cx, dy = y - cy;
        return dx * dx + dy * dy <= radius * radius;
    }
 
    private static bool InPolygon(List<Pt> poly, double x, double y)
    {
        bool inside = false;
        for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
        {
            Pt a = poly[i], b = poly[j];
            if ((a.Y > y) != (b.Y > y) &&
                x < (b.X - a.X) * (y - a.Y) / (b.Y - a.Y) + a.X)
                inside = !inside;
        }
        return inside;
    }
 
    private static double DistanceToPolyline(List<Pt> line, double x, double y)
    {
        double best = double.MaxValue;
        for (int i = 0; i + 1 < line.Count; i++)
        {
            Pt a = line[i], b = line[i + 1];
            double vx = b.X - a.X, vy = b.Y - a.Y;
            double len2 = vx * vx + vy * vy;
            double t = len2 == 0 ? 0 : Math.Clamp(((x - a.X) * vx + (y - a.Y) * vy) / len2, 0, 1);
            double dx = x - (a.X + t * vx), dy = y - (a.Y + t * vy);
            best = Math.Min(best, Math.Sqrt(dx * dx + dy * dy));
        }
        return best;
    }
 
    private static Rgb Gradient(double t)
    {
        t = Math.Clamp(t, 0, 1);
        return t < 0.5 ? Mix(Start, Middle, t / 0.5) : Mix(Middle, End, (t - 0.5) / 0.5);
    }
 
    private static Rgb Mix(Rgb a, Rgb b, double t) =>
        (a.R + (b.R - a.R) * t, a.G + (b.G - a.G) * t, a.B + (b.B - a.B) * t);
 
    private static byte ToByte(double v) => (byte)Math.Clamp(Math.Round(v), 0, 255);
}
 
