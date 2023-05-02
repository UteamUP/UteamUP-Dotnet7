using System;
using UteamUP.Shared.ModelDto;

namespace UteamUP.Shared.States
{
    public class GlobalState
    {
        public Tenant ActiveTenant { get; init; }
        public List<TenantDto> Tenants { get; init; }
        public List<InvitedUserDto> TenantsInvited { get; init; }
        public bool HasTenantInvites { get; set; }

        private string? _oid;
        public string? Oid
        {
            get => _oid;
            set
            {
                _oid = value;
                NotifyInitialized();
            }
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyInitialized();
            }
        }

        private string? _email;
        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                NotifyInitialized();
            }
        }

        public bool IsActivated { get; set; }
        public bool HasDatabaseUser { get; set; }
        public bool FirstLogin { get; set; }

        public event Action? OnInitialized;

        public void NotifyInitialized()
        {
            OnInitialized?.Invoke();
        }
    }
}