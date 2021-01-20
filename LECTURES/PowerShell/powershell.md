# Powershell (Beginner Course)

- Basic commands in Powershell
- Gathering Information
- PowerShell with remote computers
- Building a basic script

## Getting Started

- Introduction to PowerShell
- PowerShell Basics
- Gathering information with PowerShell
- Remoting with PowerShell
- Building a User Inventory Script with Powershell

## Who is this course for

- Entry-Level IT Proefessional
- IT Career Students
- 1st Level Help Desk/Desktop
- Prerequisites for the course
-- Networking basics
-- Client/Server basics
-- General troubleshooting skills

## What you don't need to know

- Scripting
- Programming

## What is PowerShell

- an execution engine that provides the ability to interface with your environment with a variety of tools

## IDE

- Powershell
- Powershell CORE
- Windows ISOC Powershell Scritping
- Visual Studio Code
- Server Management on Windows Server
- Windows Admin Center

## Commands/Examples with services

- Display all services that are stopped
- get-service | where-object Status -eq 'Stopped'
- get-service | where-object Status -eq 'Stopped' | select-object Name,Status

- using Variables
- $data = get-service | where-object Status -eq 'Stopped' | select-object Name,Status
- $data | out-file .\services.csv
- notepad .\services.csv
- $data | export-csv .\services2.csv
- $PSVersionTable
- (get-command).count

## Powershell Commands Syntax

- verb-noun (all powershell commands should follow verb-noun except custom commands)
- do something-to something
- get-verb (example)
- Parameters => used to pass information into PowerShell commands
- All parameters are called with a dash (-)
- Common ones include -ComputerName and -File
- get-verb is command that will display a listing of all of the verbs in PowerShell
- get-verb -Verb Set
- get-verb -Verb Set | format-list
- get-verb -Group Security | format-list
- start http://www.google.com
- get-service -Name M* -ComputerName Client01,DC01 => allow multiple values with a comma
- Get-Alias | more
- get-alias -Definition *service*
- help gsv
- gsv M* -Computername Client01, DC01
- gsv M* -Comp Client01, DC01
- These 3 commands are important
-- get-command
-- get-help
-- get-member
- Examples of Get-Command
-- get-command -Verb Get -Noun *DNS*
-- get-command -Name *Fire* -CommandType Function
- Examples of Get-Help
-- Get-Help -Name Get-Command -Detailed
-- man -Name Get-Command -Name Get-Command -Detailed
-- Get-Help -Name *DNS*
-- get-help to get information about the help command
-- get-help *Service*
-- help get-service
-- update-help
-- help get-service -Examples
-- help get-service -Full
-- help *about*
-- get-command -Verb New
-- get-command -CommandType Funciton | measure-object
-- get-command -Name *Ip*
-- get-command -Name *Ip* -Module Net
-- help get-ipaddress

## Documenting your work in the Powershell Console

- List the command line history
- get-history => lists all commands in the shell
- invoke-history -id [history-number]
- Get-History | out-file .\transcripts\history.txt
- notepad .\transcripts\history.txt
- clear-history
- Help Start-Transcript => captures everything that is outputted in the shell
- Get-service | Where-Object -Property Status -eq Stopped

## Objects in PowerShell

- Powershell is an object oriented language
- Not text-based
- Contain Properties and Methods
- Powershell treats data as objects
- How do you find a property in an object
-- get-service | get-member
- Pipeline => is a way of sending output of one command to a second command
- get-something | sort-something | do-something
- Pipelining in PowerShell
-- get-service | where-object status -eq "Stopped" | start-service
- get-service | get-member

- Troubleshooting made simple
-- Identifying the issues
-- Find root cause
-- Determine and implement a solution
-- Verify results
- Gathering information 
-- Computer and hardware
-- Networking
-- Files and Folders
-- get-command => to find the command I'm looking for
-- get-help or help
-- get-member => properties
- Finding a way in powershell
-- get-command
-- get-help
-- get-member
- Display all the command with Fire/NetFire
- get-command -Name get-*Fire*
- get-command -Name get-NetFire*
- help get-NetFriwWal
- Display all the members of get-NetFireWall
-- get-NetFireWall | get-member
- Fine the name of a rule by using -Name
-- get-netfirewallrule -Name *remote*
-- -Format-Table => with a pipe, displays the output as a table
- Understand this command
/*
get-netfirewallrule -Name *remotedesktop* | set-netfirewallrule -Enabled 'True' -Whatif

-Whatif => member/parameter is used to give the output as if it was executed
- The above command will retrieve all the data and then update the enabled property. The whatif parameter tells powershell that this command should not actually perform an action, but, simulate it.
*/
- Reviewing Event Log
-- get-command get-*event*
-- help get-eventlog -Examples
-- -examples => shows you how to use it
-- get-eventlog -LogName System | gm
- Example 2
-- $_ => represents the current object in the stream
-- format-table lists out machine name, user name and time generated
-- get-eventlog -log system -newest 1000 | where-object {$_.eventid -eq '1074'} | format-table machinename, username, timegenerated -autosize
- Using Get-ComputerInfo
- Files and Folders
-- help get-childitem
--- get-childitem => gets files and folders in a file system drive.
--- get-childitem -path w:\
--- get-childitem -path w:\ -recursive | gm
--- Extension => file extension
--- get-childitem -path w:\ -Recursive | where Extension -eq '.png'
--- get-childitem -path -recursive | 
where extension -eq '.png' | ft directory, name, lastwritetime
--- gcm *copy*
--- help copy-item -examples
--- copy-item w:\ -Destination c:\copyfolder -Recursive -Verbose
--- move-item w:\ -Destination c:\copyfolder -Recursive -Verbose
--- dir c:\folder -Recursive
--- rename-item c:\movedfolder -NewName c:\renamedfolder
- Scripts
-- Scripts are used to automate and build tools
-- Running scripts
-- Parameterized scripts
-- Using the powershell ise
-- Using Visual Studio Code
-- Building a remote information gathering scripts
-- script file extension should end in .ps1
-- tools for automation
-- it's not programming
-- powershell does not allow the execution of scripts
-- Get-Executionpolicy => used for checking the security policy. Powershell does not allow scripts to execute automatically.
-- help set-executionpolicy -Parameter ExecutionPolicy
--- Restricted
--- Allsigned
--- RemoteSigned
--- Bypass (THIS IS WHAT IS USED)
--- Undefined
-- Set-executionpolicy -executionpolicy bypass
- Script Basics
-- Variables 
-- Parameters
-- Logic
-- Member enumeration

- Visual Studio Code
-- Install Powershell extension language for vs code.
-- select-object for ecf next npm login
- Building a Parameterized Script
-- Run all commands as one-liners
-- Add variables and parameters
-- Add logic to run for multiple instances
-- Keep it simple
- Script Basics
-- Variables
-- Parameters (ecf-next login)
-- Logic
--- conditional
-- Member enumeration => works like select-object
- DEMO (Script Basics)
-- Remark Lines #
-- Parameterized Script
-- ECF Library NPM LOGIN
Param (
[Parameter(Manadatory=$true)]
[string[]]
$computername
)

-- for each
Foreach ($service in $services) {
$serviceStatus = $service.status
if ($serviceStatus -eq 'Running') {

Write-Output "Service OK"
} Else {
Write-Output "Check service"
}
}

- Walking through Parameterized script
-- Example
$computername = "client02"
get-service -ComputerName $computername | Where-Object -Property Status -eq 'Stopped'
- Use powershell for automating
- Resources for powershell
-- https://docs.microsoft.com for powershell
-- powershell.org
-- pluralsight
--- windows-powershell-essentials
-- Next Courses Options
--- Putting Powershell to work - Jeff Hicks
--- Automation with PowerShell Scripts by Jeff Hicks
-- http://blog.stevex.net/powershell-cheatsheet/
