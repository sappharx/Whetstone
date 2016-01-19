$repoToken = "CWDpFsufbHnIxu1NeoLm2rBuOT12lol9U"

$testDLLs = ".\Tests\Whetstone.FunctionalExtensions.Tests\bin\Debug\Whetstone.FunctionalExtensions.Tests.dll"
$filters = "+[Whetstone.*]* -[Whetstone.*.Tests]*"

# executable file paths
$nunitConsole = (Resolve-Path ".\packages\NUnit.Console.*\tools\nunit3-console.exe").ToString()
$openCover = (Resolve-Path ".\packages\OpenCover.*\tools\OpenCover.Console.exe").ToString()
$reportGenerator = (Resolve-Path ".\packages\ReportGenerator.*\tools\ReportGenerator.exe").ToString()
#$coveralls = (Resolve-Path ".\packages\coveralls.net.*\tools\csmacnz.Coveralls.exe").ToString()

& $openCover -register:user -target:$nunitConsole -targetargs:$testDLLs "-filter:$filters"
& $reportGenerator -reports:results.xml -targetdir:coverage
#& $coveralls --opencover -i results.xml --repoToken $repoToken
