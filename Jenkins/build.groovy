def getParamFromFile(path) {
	dict = [:];
	str = readFile path;
	str.readLines().each { p ->
		param = p.split("=");
		dict[param[0]] = param[1];
	};
	return dict;
}

properties(
    [
        parameters([
			string(defaultValue: 'http://localhost:5001/', name: 'APP_URL'),
			booleanParam(defaultValue: 'false', name: 'PackageLibNugget')
		])
    ]
)

node {
	cleanWs();

	def AppPackagePath = "WebApi\\bin\\Release\\netcoreapp3.1\\";
	def version;

    stage("Fetch repository"){
        git branch: "${BRANCH_NAME}", credentialsId: 'a38db37b-11d9-41fa-9348-cbbb25a8dec8', url: "https://github.com/Emile95/Ludovik";
		/*version = getParamFromFile("version.txt");
		currentBuild.displayName = "#${BUILD_NUMBER}_${version['MAJOR']}.${version['MINOR']}.${version['PATCH']}.${version['BUILD']}";*/
    }
    
    stage("Compile"){
        bat("dotnet build -c Release");
		//writeFile file: "${packagePath}\\ipAddress.txt", text: "${APP_URL}";
    }

    stage("Other") {
		//bat("copy version.txt ${packagePath}");
    }
	
	if(PackageLibNugget) {
	    def path = WORKSPACE + "\\Library\\bin\\Release\\*.nupkg";
	    
		build job: '../../../NugetAddToRepository', parameters: [
			string(name: 'PACKAGE_PATH', value: path), 
			string(name: 'REPOSITORY_PATH', value: "F:\\\\Prog\\Ludovik\\nugetPackages")
		];
	}

    archiveArtifacts "WebApi/bin/Release/netcoreapp3.1/**";
}