# Gollum

Gollum is a graphical user interface post-commit hook tool for using TortoiseSVN, ReviewBoard and BugZilla for Windows. It provides an easy way to create a diff of a commit, post the diff to ReviewBoard and update a bug on BugZilla if one was fixed in the commit. 

## Configuring Gollum

To use Gollum the URL to ReviewBoard and optionally BugZilla need to be defined in the application configuration file Gollum.exe.config. The section which needs to be changed in the file looks like this:

```xml
<Gollum.Properties.Settings>
    <setting name="ReviewBoardUrl" serializeAs="String">
		<value>https://intra/reviewboard/</value>
    </setting>
    <setting name="BugzillaUrl" serializeAs="String">
        <!-- leave empty to disable bugzilla integration -->
        <value>http://intra/bugzilla/</value>
    </setting>
</Gollum.Properties.Settings>
```

*ReviewBoardUrl* is the url to the base address of ReviewBoard, Gollum will fill the rest of the path to the API itself.
*ReviewBoardUrl* is the url to the base address of BugZilla, Gollum will fill the rest of the path to the API itself. If this is left empty the BugZilla integration will be disabled in the user interface.

## Setting up Gollum for a SVN repository

### Create a gollum.xml file in project root

This can be done easily by running Gollum without arguments. The Gollum.xml file looks like this:

```xml
<?xml version="1.0" encoding="utf-8"?>
<ProjectSettings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <ReviewBoardGroup>Gollum</ReviewBoardGroup>
  <RepositoryBasePath>/</RepositoryBasePath>
  <ReviewBoardRepositoryName>Gollum</ReviewBoardRepositoryName>
</ProjectSettings>
```

*ReviewBoardRepositoryName* is the name of the repository on ReviewBoard which corresponds to this SVN repository.
*ReviewBoardGroup* is the group on ReviewBoard to which the tickets from this SVN repository are associated with.
*RepositoryBasePath* is the difference between the SVN path defined in ReviewBoard and the path to the SVN repository.

### Install a hook script for TortoiseSVN

#### TortoiseSVN 1.7 and older

- For each working copy, add a client-side hook in TortoiseSVN settings. Enter the following information:
  - Hook Type: Post-commit hook
  - Working Copy Path
  - Command Line To Execute: The path to gollum.exe
  - Wait for the script to finish: this should be checked

#### TortoiseSVN 1.8 and later

In TortoiseSVN 1.8 (or latest trunk version) hook script can be specified in svn properties. This avoids the need to install the script for each developer individually.

- In the repository, create property with name tsvn:postcommithook
  - Fill in the dialog in the same way as done in the settings dialog
  - The program can either reside in the repository or in each developers' machine, in a directory found in the PATH variable
