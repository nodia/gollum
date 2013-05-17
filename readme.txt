
  Gollum
  ======

Installing for a project:
=========================

- Create a gollum.xml file in project root
  - This can be done easily by running gollum without arguments
- Install a hook script for TortoiseSVN (see below)


  Installing a hook script _before_ TortoiseSVN 1.8:
  ==================================================


- For each working copy, add a client-side hook in TortoiseSVN settings. Enter the following information:
  - Hook Type: Post-commit hook
  - Working Copy Path
  - Command Line To Execute: The path to gollum.exe
  - Wait for the script to finish: this should be checked


  Installing a hook script _after_ TortoiseSVN 1.8:
  =================================================

In TortoiseSVN 1.8 (or latest trunk version) hook script can be
specified in svn properties. This avoids the need to install the
script for each developer individually.

- In the repository, create property with name tsvn:postcommithook
  - Fill in the dialog in the same way as done in the settings dialog
  - The program can either reside in the repository or in each developers' machine, in a directory found in the PATH variable
