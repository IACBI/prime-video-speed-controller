Add-Type -AssemblyName System.Drawing
$ErrorActionPreference = "Stop"

$iconPath = Join-Path $PSScriptRoot "AppIcon.ico"
$size = 64

$bitmap = [System.Drawing.Bitmap]::new($size, $size)
$graphics = [System.Drawing.Graphics]::FromImage($bitmap)
$graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
$graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
$graphics.Clear([System.Drawing.Color]::Transparent)

$rect = [System.Drawing.Rectangle]::new(0, 0, $size, $size)
$brush = [System.Drawing.Drawing2D.LinearGradientBrush]::new(
    $rect,
    [System.Drawing.Color]::FromArgb(255, 3, 52, 138),
    [System.Drawing.Color]::FromArgb(255, 0, 205, 229),
    [System.Drawing.Drawing2D.LinearGradientMode]::ForwardDiagonal
)
$graphics.FillRectangle($brush, $rect)

$fontFamily = [System.Drawing.FontFamily]::new("Arial")
$primeFont = [System.Drawing.Font]::new($fontFamily, 16, [System.Drawing.FontStyle]::Bold, [System.Drawing.GraphicsUnit]::Pixel)
$videoFont = [System.Drawing.Font]::new($fontFamily, 14, [System.Drawing.FontStyle]::Bold, [System.Drawing.GraphicsUnit]::Pixel)
$format = [System.Drawing.StringFormat]::new()
$format.Alignment = [System.Drawing.StringAlignment]::Center

$graphics.DrawString("prime", $primeFont, [System.Drawing.Brushes]::White, [System.Drawing.RectangleF]::new(3, 10, 58, 20), $format)
$graphics.DrawString("video", $videoFont, [System.Drawing.Brushes]::White, [System.Drawing.RectangleF]::new(3, 28, 58, 18), $format)

$pen = [System.Drawing.Pen]::new([System.Drawing.Color]::White, 4)
$graphics.DrawArc($pen, 14, 34, 36, 20, 15, 145)
$graphics.DrawLine($pen, 47, 44, 53, 39)
$graphics.DrawLine($pen, 47, 44, 49, 52)

$graphics.Dispose()

$pngStream = [System.IO.MemoryStream]::new()
$bitmap.Save($pngStream, [System.Drawing.Imaging.ImageFormat]::Png)
$bitmap.Dispose()
$pngBytes = $pngStream.ToArray()
$pngStream.Dispose()

$fileStream = [System.IO.File]::Create($iconPath)
$writer = [System.IO.BinaryWriter]::new($fileStream)
$writer.Write([UInt16]0)
$writer.Write([UInt16]1)
$writer.Write([UInt16]1)
$writer.Write([Byte]64)
$writer.Write([Byte]64)
$writer.Write([Byte]0)
$writer.Write([Byte]0)
$writer.Write([UInt16]1)
$writer.Write([UInt16]32)
$writer.Write([UInt32]$pngBytes.Length)
$writer.Write([UInt32]22)
$writer.Write($pngBytes)
$writer.Dispose()
$fileStream.Dispose()
