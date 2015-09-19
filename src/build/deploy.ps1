$ErrorActionPreference = 'Stop'
# Environment helpers ------------------------------------
Function Get-MsBuildPath() {
    $msBuildRegPath = "HKLM:\SOFTWARE\Microsoft\MSBuild\ToolsVersions\12.0"
    $msBuildPathRegItem = Get-ItemProperty $msBuildRegPath -Name "MSBuildToolsPath"
    $msBuildPath = $msBuildPathRegItem.MsBuildToolsPath + "msbuild.exe"
    return $msBuildPath
}
# Environment variables ----------------------------------
$global_buildDirPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
$global_msBuildPath = Get-MsBuildPath
$global_nugetPath = "$global_buildDirPath\tools\nuget.exe"

$global_solutionPath = "$global_buildDirPath\..\ExamEngineService"

$global_apiSolutionFilePath = "$global_solutionPath\Exam.Api\Exam.Api.csproj"
$global_deployApiProject = "$global_buildDirPath\deploy_api.xml"

# Install nuget packages ---------------------------------
Function Install-SolutionPackages() {
    iex "$global_nugetPath restore $global_apiSolutionFilePath"
}

Function Deploy-Api() {
    iex -Command "& '$global_msBuildPath' /p:WebAppPublishDir='C:\ExamApi\' /p:Configuration=Debug '$global_deployApiProject'"
}

#=======================================  
# Shutdown Web Application
#=======================================  
Write-Host "being Shutdown Web Application...";  
iisreset /stop;  

Install-SolutionPackages
Deploy-Api

#=======================================  
# Reset IIS on the server  
#=======================================  
Write-Host "Restarting IIS Services...";  
iisreset /start;  