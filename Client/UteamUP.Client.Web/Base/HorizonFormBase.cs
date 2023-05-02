using Blazored.FluentValidation;

namespace UteamUP.Client.Web.Base;

public abstract class HorizonFormBase<TModel>
{
    [Parameter] public int? Id { get; set; }

    protected TModel _model;
    protected FluentValidationValidator? _fluentValidationValidator;
    protected int _activeStepIndex = 0;
}