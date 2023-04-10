using e_commerce_server.src.Core.Config;

var builder = WebApplication.CreateBuilder(args);

AppBundle
    .Builder()
    .ApplyBuilderContext(builder)
    .Init()
    .ApplyControllers()
    .ApplySwagger()
    .ApplyDbContext()
    .AddCors()
    .ApplyAuth()
    .Run();
