using NetArchTest.Rules;
using System.Reflection;

namespace CleanShortener.Tests;

public class ArchitecturalTests
{
    private const string DataModuleDll = "CleanShortener.Data.dll";
    private const string PresentationModuleDll = "CleanShortener.Presentation.dll";
    private const string ApplicationModuleDll = "CleanShortener.Application.dll";
    private const string DomainModuleDll = "CleanShortener.Domain.dll";

    [Fact]
    public void DomainShouldNotDependOnApplicationModule()
    {
        // arrange & act 
        var result =
            Types
            .InAssembly(Assembly.LoadFrom(DomainModuleDll))
            .ShouldNot()
            .HaveDependencyOn(ApplicationModuleDll)
            .GetResult()
            .IsSuccessful;
        
        //assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void DomainShouldNotDependOnPresentationModule()
    {
        // arrange & act 
        var result =
            Types
            .InAssembly(Assembly.LoadFrom(DomainModuleDll))
            .ShouldNot()
            .HaveDependencyOn(PresentationModuleDll)
            .GetResult()
            .IsSuccessful;

        //assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void DomainShouldNotDependOnDataModule()
    {
        // arrange & act 
        var result =
            Types
            .InAssembly(Assembly.LoadFrom(DomainModuleDll))
            .ShouldNot()
            .HaveDependencyOn(DataModuleDll)
            .GetResult()
            .IsSuccessful;

        //assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationShouldNotDependOnDataModule()
    {
        // arrange & act 
        var result =
            Types
            .InAssembly(Assembly.LoadFrom(ApplicationModuleDll))
            .ShouldNot()
            .HaveDependencyOn(DataModuleDll)
            .GetResult()
            .IsSuccessful;

        //assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationShouldNotDependOnPresentationModule()
    {
        // arrange & act 
        var result =
            Types
            .InAssembly(Assembly.LoadFrom(ApplicationModuleDll))
            .ShouldNot()
            .HaveDependencyOn(PresentationModuleDll)
            .GetResult()
            .IsSuccessful;

        //assert
        result.ShouldBeTrue();
    }
}
