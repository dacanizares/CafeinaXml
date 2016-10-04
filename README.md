# CafeinaXml

By Daniel Cañizares Corrales

This was my try to create a .Net library to persist entities using xml files.

It was useful for me in some desktop projects but the library hasn't been updated for so long. 
I made this as a student, but I hope that someone will find something helpful inside it.

# Readme

Cafeina Xml is a Microsoft.Net library that allows you to easily persist entities in the globally supported Xml standard.
It brings you a fast way to serialize, store and restore your objects with less code-lines than you ever seen.
It's user friendly and you don't need to create data enviroments in order to deploy your app, Cafeina Xml will
do everything for you.<br /><br />
Cafeina Xml will not replace a DBMS in some enviroments like heavily transactional ones, but it will help you in too many
cases to quickly create and deploy any kind of applications in web or desktop enviroments.<br /><br />
The next version we are preparing, will include features to improve multi-user access and tons of cool things that you'll love.
While we finish it, try this version of Cafeina Xml and be amazed of its easiness and power.<br /><br />
Less Sql, more fun, happy Computing!<br /><br />
Cafeina Team.
<br />
<br />
<br />
<h1>VERSION HISTORY</h1>
<hr />
<br />
<strong>RELEASE CANDIDATE (RC)</strong>
<h1>VERSION 0.4.0</h1>
01/10/2012
<br />

NEWS
<ul>            
	<li>New! you can define Lists of entities or native types (int, string...)!</li>
	<li>New! you can define Entities as properties to save!</li>
	<li>New! you can save entities without Unique or Identity attributes (Save method)!</li>
	<li>Unique type is now supported!</li>
	<li>CFXML1.0 is the new representation standard and it will be published soon</li>
	<li>To save validating Unique or Identity attributes use Insert or Update methods</li>
	<li>Simplified xml representation removing attributes from file</li>
	<li>PrimaryKey extended method removed, just use Property extended method</li>
	<li>You can change keys in your entities without any modification*</li>
	<li>Improved entities validation</li>
	<li>Added data-strutures reflection</li>
	<li>Key attribute renamed as MetaXmlAttribute (You can use it as MetaXml)</li>
	<li>Most of CafeinaXml is Microsoft Code Analysis All Rules complaining</li>
	<li>Library partially redesigned to support future improvements</li>
</ul>
FIXES
<ul>
	<li>Failed insert or update is now informed</li>
</ul>
PENDING 
<ul>
	<li>Lists of Lists currently unsupported</li>
	<li>Validate version of the file</li>
</ul>
BUGS
<ul>
	<li>When Xml file exists but is empty library crashes</li>
</ul>
<br /><br />


<strong>BETA 2 PATCH 1</strong>
<h1>VERSION 0.3.1</h1>
24/06/2012<br />
<br />
FIXED
<ul>
	<li>Uncomplete xml entities will partially load instead of throw and exception</li>
</ul>

<br /><br />

<strong>BETA 2</strong>
<h1>VERSION 0.3.0</h1>
26/04/2012
<br />
<br />
NEWS
<ul>
	<li>Added extended method string Property(Object) and PrimaryKey(Object) to XElement in order 
	  to improve DAL sintax <br /> (For more information see the official Demo)</li>
	<li>XmlDatacontext GetEntities method renamed as GetElement to clarify sintax</li>
</ul>
PENDING 
<ul>
	<li>Unique type is created but currently unsopported</li>
	<li>Validations on insert, update and delete for Unique type</li>
	<li>NotNull properties aren't supported</li>
</ul>
BUGS    
<ul>
	<li>Failed insert or update isn't informed (In revision)</li>
</ul>

<br /><br />

<strong>BETA</strong>
<h1>VERSION 0.2.0</h1>
21/02/2012
<br />
<br />
NEWS
<ul>
   <li>Reflection basic caching system to improve performance</li>
	<li>InsertOrUpdate method separated on Insert and Update methods</li>
	<li>DAL sytax is now easier than before</li>
	<li>XmlDatacontext is now the main-entry for all operations</li>
	<li>Attribute IdentifierAttribute renamed as PrimaryKey</li>
	<li>IdentifierType (Unique, Identity) renamed as PropertyType (Normal, Identity, Unique)</li>
	<li>Library internally redesigned</li>
</ul>
PENDING 
<ul>
	<li>Unique type is created but currently unsopported</li>
	<li>Validations on insert, update and delete for Unique type</li>
	<li>NotNull properties aren't supported</li>
</ul>
BUGS    
<ul>
	<li>Failed insert or update isn't informed</li>
</ul>

<br /><br />

<strong>ALPHA</strong>
<h1>VERSION 0.1.0</h1>
28/09/2011
<br />
<br />
PENDING 
<ul>
	<li>Only identity type allowed</li>
</ul>
BUGS    
<ul>
	<li>No known bugs</li>
</ul>

<br />
<br />
<br />

<h1>COMPATIBILITY ISSUES</h1>
<hr />
<br />
<h1>Why are there so many compatibility issues between versions?</h1>
<br />
Currently, Cafeina Xml it's on beta testing and some many things are changing to bring you the best developing experience. On future stable releases don't expect to many issues.
<br /><br />
<strong>RC</strong>
<h1>VERSION 0.4.x</h1>
<br />
This version makes little changes in Xml representation and code syntax. Compatibility issues with older versions.
<br /><br />
<strong>BETA 2</strong>
<h1>VERSION 0.3.x</h1>
<br />
This version makes little changes in code syntax. Small compatibility issues with older versions. There are no Xml issues.
<br /><br />
<strong>BETA</strong>
<h1>VERSION 0.2.x</h1>
<br />
Compatibility issues with oldest version.
