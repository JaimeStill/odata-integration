call dotnet run -p .\dbseeder -- -c "Server=(localdb)\ProjectsV13;Database=ODataIntegration-dev;Trusted_Connection=True;"
call dotnet build .\ODataIntegration.Web