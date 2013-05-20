# Gollum

Gollum is a graphical user interface post-commit hook tool for using TortoiseSVN, ReviewBoard and BugZilla for Windows. It provides an easy way to create a diff of a commit, post the diff to ReviewBoard and update a bug on BugZilla if one was fixed in the commit. 

## Setting up Gollum

### Configuring Gollum 

To use Gollum the URL to ReviewBoard and optionally BugZilla need to be defined in the application configuration file Gollum.exe.config. The section which needs to be changed in the file looks like this:

```xml
<userSettings>
    <!-- URLs should point to the root of the service -->
    <Gollum.Properties.Settings>
      <setting name="ReviewBoardUrl" serializeAs="String">
        <value>https://intra/reviewboard/</value>
      </setting>
      <setting name="BugzillaUrl" serializeAs="String">
        <!-- leave empty to disable bugzilla integration -->    
        <value>https://intra/bugzilla/</value>
      </setting>
    </Gollum.Properties.Settings>
  </userSettings>
```

*ReviewBoardUrl* is the url to the base address of ReviewBoard, Gollum will fill the rest of the path to the API itself.
*ReviewBoardUrl* is the url to the base address of BugZilla, Gollum will fill the rest of the path to the API itself. If this is left empty the BugZilla integration will be disabled in the user interface.

### Setting up Gollum for a SVN check out

Gollum needs to be configured separately for each SVN checkout. This can be done easily by running Gollum.exe without any arguments.
A window called "Project specific settings" will be opened. Enter the SVN checkout working copy path in the first text field and press OK.

Gollum will find the path to the gollum.exe, open the gollum.xml configuration file for the checkout in question in notepad and open 
the TortoiseSVN settings window. First the gollum.xml file needs to be configured. This is what the created Gollum.xml file looks like this:

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

Input the correct values for these three options in the gollum.xml, save the file and close it.

### Creating the post-commit hook in TortoiseSVN

Next TortoiseSVN needs to be configured to run Gollum as a post-commit hook. Depending on your TortoiseSVN version, follow the instructions 
below. After configuring TortoiseSVN, close the Gollum settings window. Gollum will now be started automatically after each commit for the configured SVN checkout.

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

## Using Gollum

Gollum can be used to create a ReviewBoard ticket in two ways.

When Gollum is started by running Gollum.exe without any arguments, just as when configuring a new SVN checkout, a previously commited revision or 
revisions can be submitted. Using the "Submit old revision" part of the "Project specific settings" window do the following:

1. First the full path to the SVN checkout directory should be given in the "Project directory" field. 
2. The "Revision from" field can be used if the diff consists of multiple commits to indicate the first revision of the ticket. 
3. The "Revision to" field is used to indicate the last revision of the ticket, or the only revision if the ticket contains only one commit.
4. Press Go and the main window of Gollum will be opened to submit the ticket.

The second way to use Gollum is the intended way as a post-commit hook tool which will be opened after each SVN commit. The main window of Gollum will be opened. Gollum 
will prompt for ReviewBoard and BugZilla user credentials when they are needed. Both APIs use authentication cookies, which will be stored in the isolated storage files 
so that the credentials do not need to be entered every time.

The first part of the window contains the SVN commit information which was received from TortoiseSVN. This information is not editable. 

The next part contains the summary, description and bugs fixed of the ReviewBoard ticket. These are automatically filled with the commit message. Review board summary will 
not allow line breaks and if one is entered Gollum will show a warning and prevent sending the ticket. The bugs fixed field will check the commit message and try to detect
if it contains bug fixes. Multiple bugs can be entered using comma as a separator. 

The third part will normally be hidden. If the Bugzilla integration is active and something is entered in the bugs fixed field the third part will be shown. The first bug 
entered in the bugs fixed field will be retrieved from Bugzilla and shown. Bug number and summary will be shown, but will not be editable. A comment can be added and the 
bug status and resolution can be changed. A default comment will be generated containing the repository name, revision, the repository path and a link to the ReviewBoard ticket
created.

Pressing the "Post review" button will first submit the ReviewBoard ticket, copy the ticket url to the user clipboard and update bugzilla is necessary.

