using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer2
{
    public class Config
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone()
            };
        }


        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("api1","my test api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    // 指定授权类型，决定了客户端使用的授权方式
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // 是否登录成功后进入授权同意页面，默认是true
                    //RequireConsent = false,

                    // 登录成功后重定向到哪里
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // 退出登录后重定向到哪里
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone // 不知何意啊，设置了，没用，感觉
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "yusheng",
                    Password = "a123456",
                    Claims = new []{
                        new Claim("name","瑜生"),
                        new Claim("website","http://aa.com"),
                        new Claim("phone","198989899")
                    }
                },
                new TestUser
                {
                    SubjectId="2",
                    Username = "yu",
                    Password = "a123456",
                    Claims = new []{
                        new Claim("name","瑜"),
                        new Claim("website","http://vv.com"),
                        new Claim("phone","198989222899")
                    }
                }
            };
        }
    }
}
