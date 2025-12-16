using ApplicationServices.Admin;
using ApplicationServices.Game.Helpers;
using Domain.Entities.Armor.Models;
using NSubstitute;

namespace TextAdventureTests.Admin;

public class AdminManagerTests
{
    [Fact]
    public async Task AdminLogin_ExecutesCreateBootsFlow()
    {
        var dbHandler = Substitute.For<IDbHandler>();
        dbHandler.AddBoots(Arg.Any<BootsEntity?>()).Returns(Task.CompletedTask);
        var manager = new AdminManager(dbHandler);

        var input = string.Join(Environment.NewLine, new[]
        {
            "yes", // enter admin flow
            "bytmig123!", // password
            "1", // create
            "1", // armor
            "1", // boots
            "5"  // exit
        });

        var originalIn = Console.In;
        var originalOut = Console.Out;

        await using var inputReader = new StringReader(input);
        await using var outputWriter = new StringWriter();

        try
        {
            Console.SetIn(inputReader);
            Console.SetOut(outputWriter);

            await manager.AdminLogin();
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }

        await dbHandler.Received(1).AddBoots(Arg.Any<BootsEntity?>());
    }

    [Fact]
    public async Task AdminLogin_IgnoresInvalidPassword()
    {
        var dbHandler = Substitute.For<IDbHandler>();
        var manager = new AdminManager(dbHandler);

        var input = string.Join(Environment.NewLine, new[]
        {
            "yes",
            "wrong-password"
        });

        var originalIn = Console.In;
        var originalOut = Console.Out;

        await using var inputReader = new StringReader(input);
        await using var outputWriter = new StringWriter();

        try
        {
            Console.SetIn(inputReader);
            Console.SetOut(outputWriter);

            await manager.AdminLogin();
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }

        await dbHandler.DidNotReceiveWithAnyArgs().AddBoots(default);
    }
}
