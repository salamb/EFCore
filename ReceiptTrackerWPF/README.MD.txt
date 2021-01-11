1/11/21
This is log as I learn c# Enitity framework,asp.net and wpf

reset framework 
	delte database
	delete migrations folder

		Install-Package Microsoft.EntityFrameworkCore.Tools
		Add-Migration InitialCreate
		Update-Database

had to add propert to cs rpoj otherwise sqlite cant find the table
<StartWorkingDirectory>$(MSBuildProjectDirectory)</StartWorkingDirectory>


Had to import using Microsoft.EntityFrameworkCore; into main window.cs
otherwise the DBSET.Load() function did not exist

need to run dbset.load() and dataview.refreshitems to have view update database changes

need to add using system.linq; to use the .where funciton on a dbset


TODO save a record
TODO load a record to a grid
TODO query a record
