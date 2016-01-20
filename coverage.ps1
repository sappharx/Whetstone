$repoToken = "CWDpFsufbHnIxu1NeoLm2rBuOT12lol9U"

$testDLLs = ".\Tests\Whetstone.FunctionalExtensions.Tests\bin\$env:CONFIGURATION\Whetstone.FunctionalExtensions.Tests.dll"
$filters = "+[Whetstone.*]* -[Whetstone.*.Tests]*"

# executable file paths
$nunitConsole = (Resolve-Path ".\packages\NUnit.Console.*\tools\nunit3-console.exe").ToString()
$openCover = (Resolve-Path ".\packages\OpenCover.*\tools\OpenCover.Console.exe").ToString()
$reportGenerator = (Resolve-Path ".\packages\ReportGenerator.*\tools\ReportGenerator.exe").ToString()
$coveralls = (Resolve-Path ".\packages\coveralls.net.*\tools\csmacnz.Coveralls.exe").ToString()

& $openCover -register:user -target:$nunitConsole -targetargs:$testDLLs "-filter:$filters"
& $reportGenerator -reports:results.xml -targetdir:coverage
& $coveralls --opencover -i results.xml --repoToken $repoToken --commitId $env:APPVEYOR_REPO_COMMIT --commitBranch $env:APPVEYOR_REPO_BRANCH --commitAuthor $env:APPVEYOR_REPO_COMMIT_AUTHOR --commitEmail $env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL --commitMessage $env:APPVEYOR_REPO_COMMIT_MESSAGE --jobId $env:APPVEYOR_BUILD_NUMBER --serviceName appveyor
