#About

This is demo project showcasing MultiTenant containers in .NET 5 and Tenant Specific services injection using Autofac MultiTenant library.

### Tenant Resolution
Tenant resolution is done using HeaderResolutionStrategy.


### How to use
Its already a ready boiler plate code. You can directly start using it. 

Modify MemoryTenantStore.cs to change Tenant Resolution. 

In Startup.cs, you can configure tenant specific services using 'multitenantContainer.ConfigureTenant'. 

As an example, a OperationIdService singleton returns GUID for each tenant. 