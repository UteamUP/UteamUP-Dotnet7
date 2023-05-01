global using Microsoft.EntityFrameworkCore;
global using AutoMapper;
global using System.Reflection;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.Identity.Web;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.OpenApi.Models;

// UteamUP Database
global using UteamUP.Server.Database.Contexts;

// UteamUP Shared
global using UteamUP.Shared.Models;
global using UteamUP.Shared.ModelDto;

// Generic Repository
//Global using UteamUP.Repository.GlobalRepository.Interfaces;
//Global using UteamUP.Repository.GlobalRepository.Implementations;

// Global Repository
global using UteamUP.Server.Repository.GlobalRepository.Interfaces;
global using UteamUP.Server.Repository.GlobalRepository.Implementations;