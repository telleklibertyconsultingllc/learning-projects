# Authentication and Authorization in ASP.Net Core
-- OpenIdConnect
--- Browser => Client => Identity Provider
---- Client Id + Secret => checks if client id and secret is known. Uses certificates
---- Scope => collection of claims or one API (https://4sh.nl/clientconfig)
----- OpenId Scope
----- Identity or Profile Scope
----- Custom Scope
----- API Resource Scope => defined per APIs
----- Clients => contains the client id and secret
------ AllowedScopes
---- Response Type => response (depends on the flow). Can't request any response type
---- Redirect URI => where to go when logged in
-- Redirect to Redirect URI when successfully logged in
--- c#
public void ConfigureServices() {
	services.AddControllersWithViews(o => o.Filters.Add(new AuthorizeFile()));
	services.AddAuthentication(o => {
		o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		o.DefaultchallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
	})
	.AddCookie()
	.AddOpenIdConnect(options => {
		options.Authority = "https://localhost:5000"; /// URL of identity provider
		options.ClientId = "confarch_web";            /// Use application secrets
		options.ClientSecret = "{{GUIDID}}";
		options.CallbackPath = "/signin-oidc";
		
		/// Claims and API scope
		options.Scope.Add("confarch");
		options.Scope.Add("confarch_api");
		/// Access Token must be saved
		options.SaveTokens = true;
		/// Enables extra request to get more information
		options.GetClaimsFromUserInfoEndpoint = true;
		
		options.ClaimActions.MapUniqueJsonKey("CareerStarted", "CareerStarted");
		options.ClaimActions.MapUniqueJsonKey("FullName", "FullName");
		options.ClaimActions.MapUniqueJsonKey("FullName", "FullName");
		
		/// Response Type or code flow (Authorization Code Flow)
		options.ResponseType = "code";
		options.ResponseMode = "form_post";
		
		options.UsePkce = true;
	});
}

## Choosing an OpenIdConnect Flow
- Interaction with authorization endpoint via browser
- Form post rather than query string
- Front channel considered unsafe

- Authorization Code Flow
-- Code is sent to client to do a back channel request 
-- Code is exchanged as a token
-- A client id and secret is required to get the code/token
-- Code Substitution Attack (where someone intercepts a token and uses it)
-- PKCE (PICK-CEE) => enables an encrypted key to send with the code or token
-- Back Channel still used
-- Tokens exposed in browser
-- Authorization code flow with PKCE still safer than other options
-- Public client
-- Client Secret pointless
-- Can be turned off (PKCE)
-- Mobile + desktop applications are also public clients

- Other flows (NOT IMPORTANT) https://4sh.nl/flows
-- Implicit 
-- Hybrid (Authorization Flow + PKCE)

## Which Flow Should I Use?
- The general advice is to use the authorization code flow with PKCE for all types of applications. This flow requires the least amount of effort for the client application to implement while giving the best level of security. This is the recommendation that is being proposed for OAuth 2.1.
- If the client application cannot support PKCE, I recommend falling back to using the hybrid flow response type of “code id_token” with nonce and c_hash validation.
- Simple
-- Is Interactive Client? YES: Authorization Code + PKCE; NO: Client Credentials

## Response Types by Flow
Flow				Response Types
Authorization Code	code
Implicit			id_token
Implicit			id_token token
Hybrid				code id_token
Hybrid				code token
Hybrid				code id_token token

- Single SignOn comes into play when you have multiple sites using the same identity provider

## Protecting and Calling an API Access Token
- C#
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options => 
	{
		options.Authority = "https://localhost:5000"; /// Identity Provider this API has to trust
		options.Audience = "confarch_api";			  /// Each API has to check for in the Audience Claim  
	});
- Above line will require an Access Token in each header or else it will throw an 401 status code
- Below line will configure all API calls to have access token

## Authentication Types for APIs
- Cookies
-- Subject to request forgery
- Basic Auth => atob()
-- Not secure, unless enforcing SSL
-- Passing a credential across each request
-- Increases surface area of attacks
- Token Aut
-- Most common
-- Industry standard Tokens are easy
-- Should expire than cookies
-- Uses JSON Web Tokens (JWTs)
--- Industry Standard
--- Self-contained, small and complete
---- User information
---- Claims
---- Validation Signature
- OAuth
-- Use trusted third-party to identify users

