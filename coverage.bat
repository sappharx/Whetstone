
.\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe -target:runtests.bat -register:user -filter:"+[Whetstone.*]* -[Whetstone.*.Tests]*"

.\packages\ReportGenerator.2.3.5.0\tools\ReportGenerator.exe -reports:results.xml -targetdir:coverage
