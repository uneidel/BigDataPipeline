<# .synopsis
    Prepare ADL

#>

Function DeGZip-File{
    Param(
        $infile,
        $outfile = ($infile -replace '\.gz$','')
        )

    $input = New-Object System.IO.FileStream $inFile, ([IO.FileMode]::Open), ([IO.FileAccess]::Read), ([IO.FileShare]::Read)
    $output = New-Object System.IO.FileStream $outFile, ([IO.FileMode]::Create), ([IO.FileAccess]::Write), ([IO.FileShare]::None)
    $gzipStream = New-Object System.IO.Compression.GzipStream $input, ([IO.Compression.CompressionMode]::Decompress)

    $buffer = New-Object byte[](1024)
    while($true){
        $read = $gzipstream.Read($buffer, 0, 1024)
        if ($read -le 0){break}
        $output.Write($buffer, 0, $read)
        }

    $gzipStream.Close()
    $output.Close()
    $input.Close()
}

$localPath = "c:\\temp\";

if ((Test-Path $localPath) -eq $false)
{
    New-Item -ItemType Directory $localPath -Force;
} 

#Check Path

$webclient = new-object System.Net.WebClient;
$targetpath = Join-Path -Path $localPath -ChildPath "lastfm.gz";
$webclient.DownloadFile("http://mtg.upf.edu/static/datasets/last.fm/lastfm-dataset-360K.tar.gz", $targetpath);

DeGZip-File -infile $targetpath -outfile "c:\local\lastfm"
