using Masuit.Tools;
using Masuit.Tools.AspNetCore.ModelBinder;
using Masuit.Tools.Files;
using Masuit.Tools.Media;

//var stream = File.Open(@"C:\Users\CK\Pictures\Î¢ÐÅÍ¼Æ¬_20230206155042.jpg", FileMode.Open, FileAccess.ReadWrite);
//var watermarker = new ImageWatermarker(stream);
//var ms = watermarker.AddWatermark(File.OpenRead(@"C:\Users\CK\Pictures\89b6c60f91820ffb8a4d9640b47055cd5d213aca49294-alMMCu_fw1200.jpg"), 0.5f);
//ms.SaveFile(@"C:\Users\CK\Pictures\1.jpg");

bool isInetAddress = "114.114.114.114".MatchInetAddress();
DateTime dt = "2023/03/13".ToDateTime();

Console.WriteLine(dt);
Console.WriteLine(1);
Console.ReadKey();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options => options.ModelBinderProviders.InsertBodyOrDefaultBinding());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
