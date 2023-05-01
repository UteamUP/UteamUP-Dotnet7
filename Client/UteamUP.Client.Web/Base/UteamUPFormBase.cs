using Blazored.FluentValidation;

namespace UteamUP.Client.Web.Base;

public class UteamUPFormBase<TModel> : UteamupComponentBase
{
    [Parameter] public int? Id { get; set; }
    [Parameter] public InitComponent? InitComponent { get; set; }

    protected TModel _model;
    protected FluentValidationValidator? _fluentValidationValidator;
    protected int _activeStepIndex = 0;
}