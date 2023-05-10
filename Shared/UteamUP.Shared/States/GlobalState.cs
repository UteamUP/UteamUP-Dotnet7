using System;
using UteamUP.Shared.ModelDto;

namespace UteamUP.Shared.States
{
    public class GlobalState
    {
        private bool _isUpToDate;
        public bool IsUpToDate
        {
            get => _isUpToDate;
            set
            {
                _isUpToDate = value;
                NotifyInitialized();
            }
        }
        private Tenant? _activeTenant;
        public Tenant? ActiveTenant {
            get => _activeTenant;
            set
            {
                _activeTenant = value;
                NotifyInitialized();
            }
        }
        
        public List<Tenant> Tenants { get; set; }
        public List<Tenant> _tenantsInvited;
        public List<Tenant> TenantsInvited {
            get => _tenantsInvited;
            set
            {
                _tenantsInvited = value;
                NotifyInitialized();
            }
        }
        public bool HasTenantInvites { get; set; }
        public int DefaultTenantId { get; set; }

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

        /*
        public GlobalState(
            string name, 
            Tenant? _activeTenant, 
            string? _oid, 
            string? _email, 
            List<Tenant> _tenantsInvited, 
            bool _isActivated, 
            List<Tenant> _tenants,
            bool _hasTenantInvites,
            int _defaultTenantId,
            bool _hasDatabaseUser,
            bool _firstLogin
            )
        {
            Name = name;
            ActiveTenant = _activeTenant;
            Oid = _oid;
            Email = _email;
            TenantsInvited = _tenantsInvited;
            IsActivated = _isActivated;
            Tenants = _tenants;
            HasTenantInvites = _hasTenantInvites;
            DefaultTenantId = _defaultTenantId;
            HasDatabaseUser = _hasDatabaseUser;
            FirstLogin = _firstLogin;
        }
        */
    }
}