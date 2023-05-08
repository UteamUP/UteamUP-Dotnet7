clean:
	rm -rf Client/UteamUP.Client.Web/bin
	rm -rf Client/UteamUP.Client.Web/obj
	rm -rf 'Client/UteamUP.Client.Web/bin 2'
	rm -rf 'Client/UteamUP.Client.Web/obj 2'

	rm -rf Client/UteamUP.Client.Repository/bin
	rm -rf Client/UteamUP.Client.Repository/obj
	rm -rf 'Client/UteamUP.Client.Repository/bin 2'
	rm -rf 'Client/UteamUP.Client.Repository/obj 2'

	rm -rf Client/UteamUP.Client.Components/bin
	rm -rf Client/UteamUP.Client.Components/obj
	rm -rf 'Client/UteamUP.Client.Components/bin 2'
	rm -rf 'Client/UteamUP.Client.Components/obj 2'

	rm -rf Server/UteamUP.Server.Api/bin
	rm -rf Server/UteamUP.Server.Api/obj
	rm -rf 'Server/UteamUP.Server.Api/bin 2'
	rm -rf 'Server/UteamUP.Server.Api/obj 2'

	rm -rf Server/UteamUP.Server.Database/bin
	rm -rf Server/UteamUP.Server.Database/obj
	rm -rf 'Server/UteamUP.Server.Database/bin 2'
	rm -rf 'Server/UteamUP.Server.Database/obj 2'
	
	rm -rf Server/UteamUP.Server.Repository/bin
	rm -rf Server/UteamUP.Server.Repository/obj
	rm -rf 'Server/UteamUP.Server.Repository/bin 2'
	rm -rf 'Server/UteamUP.Server.Repository/obj 2'

	rm -rf Server/UteamUP.Server.Services/bin
	rm -rf Server/UteamUP.Server.Services/obj
	rm -rf 'Server/UteamUP.Server.Services/bin 2'
	rm -rf 'Server/UteamUP.Server.Services/obj 2'

	dotnet clean
watch:
	dotnet clean
	dotnet watch --project Server/UteamUP.Server.Api/Uteamup.Server.Api.csproj --launch-profile https
run:
	dotnet run --project Server/UteamUP.Server.Api/Uteamup.Server.Api.csproj --launch-profile https
build:
	dotnet build
clean-idea:
	rm -rf .idea
