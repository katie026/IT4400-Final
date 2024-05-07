Used this tutorial:
https://malcolmmaima.medium.com/how-to-export-data-from-firebase-firestore-database-in-json-format-using-node-18b73e181c09

1. Exporting data from Firestore
- To export data from your Firestore database, follow these steps:
    - Open the Firebase Console.
    - Navigate to your project and click the gear icon next to the “Project Overview” heading.
    - Click the “Project Settings” button.
    - In the “Service accounts” tab, click the “Generate new private key” button. This will download a JSON file with your Firebase project’s configuration.
    - Rename the downloaded file to appConfig.json.
    - Open a terminal and navigate to the directory where you want to save the exported JSON file. Make sure the above file you just renamed exists in the same directory as well.
    - Run the following command to export data from your Firestore database:
    npx -p node-firestore-import-export firestore-export -a appConfig.json -b backup.json

Options
The firestore-export command has several options that you can use to customize the export process. Here are some of the most commonly used options:
-a, --appConfig: Specifies the path to the JSON file that contains your Firebase project's configuration. If you don't specify this option, the command will look for a file named appConfig.json in the current directory.
-b, --backup: Specifies the path to the JSON file where the exported data will be saved. If you don't specify this option, the command will create a file named backup.json in the current directory.
-c, --collection: Specifies the name of a specific collection to export. If you don't specify this option, the command will export all collections in the database.
-h, --help: Displays the command's usage and options.
-l, --limit: Specifies the maximum number of documents to export from each collection.
-p, --pretty: Formats the JSON output in a more human-readable format.
