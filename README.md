# ImageSearchProject
==================================================
MVVM WPF approach taken to build the application
==================================================

Code base description:

Repository has 2 solutions - Interfaces.sln and Assessment.sln 

1) Build order = Interfaces.sln and then followed by Assessment.sln
2) All the deliverables will be placed in "Repository/Bin" folder
3) 'Assessment.ImageSearch.exe' being the entry point, it needs 2 configurations to be entered in 'Assessment.ImageSearch.exe.config'
     a) ImagesDownloadPath - a folder location on system, where the images fetched can be downloaded (provide path with writable permission)
     b) SearchMode - 2 modes of search text combination : Any / All
4) Logging is not complete, as I am facing some issues with log4net integration. Have plans to switch to EventLogs.
5) Used NUnit framework for sample unit test cases added. 
6) Internal interfaces are all part of 'Assessment.sln' projects.
7) 3rd party dlls are placed externally and referenced from the root folder. Not all projects use Nuget packages.
