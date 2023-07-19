  # aspnet-b8-shamimhosen
Asp.Net Batch-8 Main Repository which is used for Class Task(Assignment, Exam, Project)


## [Class-10 (New Syllabus & SASS)](https://docs.google.com/document/d/1A9cbTsqpL61j4-cdMniQUc9Eqo92qgTaHyxadCoJ4oU/edit) 
* New Syllabus (30)
* Injection Type(61)--Constractor, Property, Methode Dependency Injection--Constractor most populler
* SCSS File Complie(82) 
* We can nest style:(95)
* Split/Import File:(97)
* Mixin:(100)
* OMR(115) -- ado.net, entity framework, dapper, inhybernet
## Class-30 Identity Framwork
1. Add IdentityDbContext(30) & paramiter(55)
   <details>
    <summary>ApplicationDbContext</summary>
    
    ```c#
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>, IApplicationDbContext
    {     }
    ```
   </details>
2. Add Membership class-31\
   ![image](https://github.com/Shamim448/aspnet-b8-shamimhosen/assets/43339514/37cd35f6-003c-4892-8174-2388a90cb733)
3. Modify ServiceCollectionExtention> AddIdentity-37\
   আমরা প্রোগ্রাম ডট সিএস থেকে যে আইডেন্টিটি ডিফল্ট যে মেথডটা নিয়ে এসে একটা একটা সার্ভিস কালেকশন তৈরি করেছি সেখানে যে ম্যাপটা তৈরি করেছি staticদিয়ে তার ভিতরে কিছু কনফিগারেশন 
   আমাদের দিয়ে দিতে হবে. যার ভিতরে বিভিন্ন ধরনের লগইন logout এই পাথরগুলো থাকবে এবং কুকি থাকবে তারপরে গিয়ে পাসওয়ার্ড সেটিং থাকবে লক আউট সেটিং থাকবে এই টাইপের কোড 
   এবং নিচে সার্ভিস ডট রেজাল্ট পেজ এটা দিয়ে দিতে হবে services.AddRazorPages();
   <details>
    <summary>ServiceCollectionExtention</summary>
    
    ```c#
    namespace Crud.Persistance.Extentions
    {
    public static class ServiceCollectionExtentions
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                    options.LogoutPath = new PathString("/Account/Logout");
                    options.Cookie.Name = "FirstDemoPortal.Identity";
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            services.AddRazorPages();
        }
      }
    }
    ```
   </details>
4. Edit _LoginPartial-43\
   যখন আমরা উপরের কাজগুলো করব তখন লগ ইন পার্শিয়াল এর মধ্যে আইডেন্টিটি ইউজার থাকবে সেটা চেঞ্জ করে যেহেতু
   আমরা আইডেন্টিটি ইউজারের পরিবর্তে অ্যাপ্লিকেশন ইউজার ক্লাস তৈরি করেছি 
   সুতরাং সেখানেও আইডেন্টিটি টিউজারের পরিবর্তে অ্যাপ্লিকেশন user দিয়ে দিতে হবে
   <details>
    <summary>Dummy</summary>
    
    ```c#
    @using Crud.Persistance.Features.Membership;
    @using Microsoft.AspNetCore.Identity
    @inject SignInManager<ApplicationUser> SignInManager
    @inject UserManager<ApplicationUser> UserManager
    ```
   </details>
6. Add this line in Program.cs - 47\
   app.UseAuthentication();
7. Note\
   এবার বিল্ড দিয়ে প্রজেক্ট চেক করে নিয়ে মাইগ্রেশন জেনারেট করতে পারি যদি মাইগ্রেশন জেনারেট না হয় তাহলে আইডেন্টিটির জন্য যে ডিফল্ট একটা মাইগ্রেশন তৈরি হয় জিরো জিরো জিরো দিয়ে এগুলো 
   ডিলিট করতে হবে তারপরে মাইগ্রেশন genite করে চেক করতে হলে যে আমাদের এনটিটি টেবিল এবং আইডেন্টিটি টেবিল দুইটাই তৈরি হয়েছে কিনা এখানে প্রবলেম হতে পারে শুধু আইডিটি টেবিল তৈরি 
   হতে পারে কিন্তু আমাদের যে এন্টিটি আছে সেটা তুই নাও হতে পারে সে ক্ষেত্রে নিচের এই কোডটুকু দিয়ে দিতে হবে ai package doita lagbe

   <details>
    <summary>ApplicationDbContext</summary>
    
    ```c#
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(UserSeed.Users);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            // Add other entity configurations if needed

        }
    ```
   </details>
8. Add Scaffold Item -89\
   web project >> add >>New Scaffolded Item
  then select login and register page and select dbcontext then create
  এখানে যদি কোন এরর শুরু করে যে প্রজেক্ট লোডফিল্ড বা কনফিগারেশন ফিল্ড তাহলে আমাদের কোর্ট জেনারেশন ডিজাইন এবং কোড জেনারেশন অন util  এই দুইটা প্যাকেজ ম্যানেজার আপডেট করতে 
  হবে\
  Microsoft.AspNetCore.Identity.UI\
  Microsoft.EntityFrameworkCore.Design



> Summery:  আমাদের অ্যাপ্লিকেশন ডিবি কনটেক্সট ক্লাসে যে ইনভাইট করা হয়েছে ডিবি কোনটেক্স থেকে সেটা চেঞ্জ করে আইডেন্টিটি ডিবি কন্টেক্স সেট করে দিতে হবে.  তারপর persistence এর ভিতরে মেম্বারশিপ এর জন্য কিছু ক্লাস আছে যেগুলো কপি পেস্ট করে দিয়ে দিব.
তারপর আমরা যে Addidentity  এর জন্য যে সার্ভিস কালেকশন এক্সটেনশন তৈরি করেছি তার ভিতরে কিছু কনফিগারেশন দিয়ে দিতে হবে আগেরগুলো রিমুভ করে যার মধ্যে আমাদের তৈরি করা অ্যাপ্লিকেশন রোল ইউজার ইউজার ম্যানেজার এগুলো থাকবে লগইন, লগ আউট কুকীএগুলোর পাত থাকবেএবং পাসওয়ার্ড এর কিছু রিকোয়ারমেন্ট থাকবে এবং একাউন্ট লগের কিছু রিকোয়ারমেন্ট থাকবে,
তারপরে লগইন পার্শিয়াল যে ফাইল টা আছে সেই ফাইলে কিছু মডিফাই করে দিতে হবে  আইডেন্টিটি ইউজারের পরিবর্তে আমাদের তৈরি করা অ্যাপ্লিকেশন ইউজার দিয়ে দিতে হবে. এবার আমাদের অ্যাপ্লিকেশন ডিবি কনটেক্স এ আইডেন্টিটি ডিবি context ভিতরে কিছু প্যারামেটার দিয়ে দিতে হবে এবং প্রোগ্রাম ডট সিএসের ভিতরে গিয়ে অ্যাপ ডট ইউজ অথেন্টিকেশন এটা দিয়ে দিতে হবে এবার আমরা যদি ওয়েব প্রজেক্ট থেকে এড স্কাবলড আইটেম দিয়ে লগইন এবং রেজিস্টার পেজ ক্রিয়েট করতে পারি তবে সে ক্ষেত্রে এগুলো রেজার পেজ হবে এই জন্য আমাদেরকে সার্ভিস কালেকশনের নিচে addrazorpage দিয়ে দিতে হবে
স্কাবলড আইটেম তৈরি করতে গেলে আমাদের কোন জেনারেটর ডট utils এবং কোড জেনারেটর ডট design এই দুইটা প্যাকেজ আপডেট করে রাখতে হবে 

   <details>
    <summary>Dummy</summary>
    
    ```c#
    
    ```
   </details>

   
## Class-31 Register Page Convert
1. Create Account Controller\
   কন্ট্রোলারের ভিতরে গেট এবং পোস্ট মেথডের জন্য IAcctionResult  থাকবে এবং কিছু ডিপেন্ডেন্সি ইনজেকশন থাকে
   <details>
    <summary>Account Controller(26) and add dependency(33,36)</summary>
    
    ```c#
    namespace Crud.web.Controllers
    {
    public class AccountController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IEmailSender _emailSender;
        public AccountController(ILifetimeScope scope, ILogger<AccountController> logger, 
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _scope = scope;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;

        }
         public async Task<IActionResult> RegisterAsync(string returnUrl = null)
        {
            var model = _scope.Resolve<RegisterModel>();
            model.ReturnUrl = returnUrl;
            //model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            model.ReturnUrl??= Url.Content("~/");
            //model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { 
                    UserName = model.Email, 
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                    pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = model.ReturnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href  ='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(model.ReturnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
           }
        
           }
        }
    ```
   </details>
2. Create Register Model Class\
   এই মডেলের ভিতরে আমরা রাজর যে ভিউটা আছে ভিউ থেকে from ভ্যালুগুলো নিয়ে আসবো, এই মডেলের ভিতরে আমরা রাজর যে ভিউটা আছে ভিউ থেকে from ভ্যালুগুলো নিয়ে আসবো,
   এবং নিচে যে রিটার্ন ইউআরএল আছে সেটার nullable করে দিব **(67)**  public string? ReturnUrl { get; set; }
   যদি আমরা আরো কোন ফিল্ড প্রয়োজন মনে করি তাহলে এখানে অ্যাড করতে পারব যেমন আমরা এড করেছি ফাস্ট নেম এবং লাস্ট নেম 

   <details>
    <summary>Create Register Model Class(27)</summary>
    
    ```c#
    namespace Crud.web.Models
    {
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string LastName { get; set; }

        public string? ReturnUrl { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }
      }
    }
    ```
   </details>
   <details>
    <summary>RegisterModule Binding to WebModule For DI(34)</summary>
    
    ```c#
    public class WebModule : Module
    {      
        
        protected override void Load(ContainerBuilder builder)
        { 
            builder.RegisterType<RegisterModel>().AsSelf().InstancePerLifetimeScope();
        }
    }
    ```
   </details>
3. Disable External Authentication-37
4. Add Extra input field in ResponseModel-45
5. Create View Page-51\
   ভিউপেজ ক্রিয়েট করে রাজর ভিউ পেজ থেকে সবকিছু কপি করে নিয়ে আসতে হবে তারপরে উপরে যে আমরা রেজিস্টার মডেল তৈরি করেছি সেটা মডেলের মধ্যে দেখিয়ে দিতে হবে এবং আগে যে পেজ      লেখা ছিল সেটার রিমুভ করে দিতে হবে এবং আমরা যদি এক্সট্রা কোন ফিল্ড এড করে থাকি মডেলের ভিতরে সেগুলোও এখানে নিচে অ্যাড করে দিতে হবে অবশ্য ভিউ পেস্টটা হবে রুট view মধ্যে কোন 
   এরিয়ার মধ্যে নয়,  কারণ আমরা root থেকেই লগইন করাবো\
   এবং form উপরে  এরিয়া,  কন্ট্রোলার,  একশন এইগুলো বসায় দিব **(55)**
   <details>
    <summary>View Page</summary>
    
    ```HTML
    @model RegisterModel
      @{
    ViewData["Title"] = "Register";
      }

   <h1>@ViewData["Title"]</h1>

   <div class="row">
    <div class="col-md-4">
        <form id="registerForm" asp-action="register" asp-controller="account" asp-area="" asp-asp-antiforgery="true" asp-route-returnUrl="@Model.ReturnUrl" method="post">
            <h2>Create a new account.</h2>
            <hr />
            <div asp-validation-summary="ModelOnly" class="text-danger" role="alert"></div>
            <div class="form-floating mb-3">
                <input asp-for="Email" class="form-control" autocomplete="username" aria-required="true" placeholder="name@example.com" />
                <label asp-for="Email">Email</label>
                <span asp-validation-for="Email" class="text-danger"></span>
            </div>
            <div class="form-floating mb-3">
                <input asp-for="Password" class="form-control" autocomplete="new-password" aria-required="true" placeholder="password" />
                <label asp-for="Password">Password</label>
                <span asp-validation-for="Password" class="text-danger"></span>
            </div>
            <div class="form-floating mb-3">
                <input asp-for="ConfirmPassword" class="form-control" autocomplete="new-password" aria-required="true" placeholder="password" />
                <label asp-for="ConfirmPassword">Confirm Password</label>
                <span asp-validation-for="ConfirmPassword" class="text-danger"></span>
            </div>
            <div class="form-floating mb-3">
                <input asp-for="FirstName" class="form-control" autocomplete="username" aria-required="true" placeholder="Shamim" />
                <label asp-for="FirstName">First Name</label>
                <span asp-validation-for="FirstName" class="text-danger"></span>
            </div>
            <div class="form-floating mb-3">
                <input asp-for="LastName" class="form-control" autocomplete="username" aria-required="true" placeholder="Hosen" />
                <label asp-for="LastName">Last Name</label>
                <span asp-validation-for="LastName" class="text-danger"></span>
            </div>
            <button id="registerSubmit" type="submit" class="w-100 btn btn-lg btn-primary">Register</button>
        </form>
       </div>

      </div>

      @section Scripts {
       <partial name="_ValidationScriptsPartial" />
      }
    ```
   </details>
> আমরা একটা কন্ট্রোল তৈরি করব এবং একটা মডেল ক্লাস তৈরি করব,  মডেল ক্লাসে rezor page  ভিতর থেকে ফর্মটা নিয়ে আসবো এবং সেখানকার গেস্ট এবং পোস্ট মেথডগুলো কন্ট্রোলারে নিয়ে আসবো 
 এবং সেগুলোর error  রিমুভ করব সে ক্ষেত্রে আমাদের কিছু ডিপেন্ডেন্সির প্রয়োজন হবে এবং সে ডিপেন্ডেন্সি টা দিয়ে error রিমুভ করে আমরা ওয়েব মিডিয়েলের সঙ্গে Dependency injection করে দিব 
 তারপরে একটা ভিউ পেজ ক্রিয়েট করব যেটা মডেল থেকে আসবে এবং ভিউ পেজের কনটেন্ট আসবে রেজোড়ের ভিউ পেজ থেকে তারপর সেখানে এরিয়া কন্ট্রোলার অ্যাকশন এগুলো দেখিয়ে দেব form ar 
 vitor এবং মডেল ক্লাসের ভিতরে আমাদের রিটার্ন ইউআরএল টা nullable এবং সেট করে দিতে হবে এবং কন্ট্রোলারে একটা ভিউ মডেল রিটার্ন করবে আই যেটা IAcction return করবে, ইমেল স্যান্ডার 
 এবং এক্সটার্নাল লগইন এগুলো ডিজাবুল করে দিব

## Class-32 Login-Logout Page
1. Create LoginModel-3\
    Copy inpitmethon property from login.cshtml.cs mvvm page and past Loginmodel also copy-past returnurl
    <details>
     <summary>LoginModel</summary>
    
     ```c#
     namespace Crud.web.Models
     {
     public class LoginModel
     {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
     }
     }   
     ```
    </details>
2. Register LoginModel into WebModule - 5
   <details>
    <summary>Dummy</summary>
    
    ```c#
    protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserCreateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserUpdateModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RegisterModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoginModel>().AsSelf().InstancePerLifetimeScope();
        }
    ```
    </details>

3. Add Login IAction in AccountController-7\
    Copy Get and post method from login.cshtml.cs and past to AccountController
    <details>
     <summary>Add IAcction into AccountController</summary>
    
     ```c#
    //Login Page
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            var model = _scope.Resolve<LoginModel>();
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            model.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(model.ReturnUrl);
                }
                
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt."); 
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
     ```
    </details>

4. Login View Page-14\
    copy login razor page login MVC page and remove error 
   <details>
     <summary>Login MVC Page</summary>
    
     ```c#
    @model LoginModel

    @{
    ViewData["Title"] = "Log in";
    }

    <h1>@ViewData["Title"]</h1>
    <div class="row">
    <div class="col-md-4">
        <section>
            <form id="account" method="post" asp-antiforgery="true" asp-controller="Account" asp-action="Login">
                <h2>Use a local account to log in.</h2>
                <hr />
                <div asp-validation-summary="ModelOnly" class="text-danger" role="alert"></div>
                <div class="form-floating mb-3">
                    <input asp-for="Email" class="form-control" autocomplete="username" aria-required="true" placeholder="name@example.com" />
                    <label asp-for="Email" class="form-label">Email</label>
                    <span asp-validation-for="Email" class="text-danger"></span>
                </div>
                <div class="form-floating mb-3">
                    <input asp-for="Password" class="form-control" autocomplete="current-password" aria-required="true" placeholder="password" />
                    <label asp-for="Password" class="form-label">Password</label>
                    <span asp-validation-for="Password" class="text-danger"></span>
                </div>
                <div class="checkbox mb-3">
                    <label asp-for="RememberMe" class="form-label">
                        <input class="form-check-input" asp-for="RememberMe" />
                        @Html.DisplayNameFor(m => m.RememberMe)
                    </label>
                </div>
                <div>
                    <button id="login-submit" type="submit" class="w-100 btn btn-lg btn-primary">Log in</button>
                </div>
                <div>
                    <p>
                        <a id="forgot-password" asp-area="" asp-controller="Account" asp-action="ForgotPassword">Forgot your password?</a>
                    </p>
                    <p>
                        <a asp-area="" asp-controller="Account" asp-action="Register" asp-route-returnUrl="@Model.ReturnUrl">Register as a new user</a>
                    </p>
                    <p>
                        <a id="resend-confirmation" asp-area="" asp-controller="Account" asp-action="ResendEmailConfirmation">Resend email confirmation</a>
                    </p>
                </div>
            </form>
        </section>
        </div>

    </div>

        @section Scripts {
     <partial name="_ValidationScriptsPartial" />
        }
     ```
    </details>


5. Edit LoginPartial-27\
    asp route add (area, controller, action)
   <details>
     <summary>LoginPartial Add</summary>
    
     ```c#
    @using Crud.Persistance.Features.Membership;
    @using Microsoft.AspNetCore.Identity
    @inject SignInManager<ApplicationUser> SignInManager
    @inject UserManager<ApplicationUser> UserManager

    <ul class="navbar-nav">
    @if (SignInManager.IsSignedIn(User))
    {
    <li class="nav-item">
        <a  class="nav-link text-dark"  asp-area ="Admin" asp-controller="Dashboard" asp-action="Index"  title="Manage">Hello @User.Identity?.Name!</a>
    </li>
    <li class="nav-item">
        <form  class="form-inline" asp-antiforgery="true" asp-area=" " asp-controller="Account" asp-action="Logout" asp-route-returnUrl="@Url.Action("Index", "Home", new { area = "" })">
            <button  type="submit" class="nav-link btn btn-link text-dark">Logout</button>
        </form>
    </li>
    }
    else
    {
    <li class="nav-item">
            <a class="nav-link text-dark" asp-area=" " asp-controller="Account" asp-action="Register">Register</a>
    </li>
    <li class="nav-item">
            <a class="nav-link text-dark" asp-area=" " asp-controller="Account" asp-action="Login">Login</a>
    </li>
    }
    </ul>
     ```
    </details>

6. Logout Scaffold and IAction-35\
    Create a New Scaffold item Logout and post IAction past to Account controller
   <details>
     <summary>Logout IAction into AccountController</summary>
    
     ```c#
    public async Task<IActionResult> LogoutAsync(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
     ```
    </details>
7. Restriction Apply in Dashboard-60\
    Admin area could not access without login, if we add ** Authorize ** anotation in top of the 
    sepecific controller then we can't access without login all of actioe under the controller.
    But ws need access specific action without action need ** Anonymouse ** anotation top of the action
   <details>
     <summary>Area Access Restriction</summary>
    
     ```c#
     [Area("Admin"), Authorize] //used for restriction
     [AllowAnonymous]//allow without login
     ```
    </details>
  Note: Authontication: Check userd login or not
        Authorization: Check user permited the visit an area

## Class-33 (Role-Management)
1. SettingController Create(10,21,29)\
    এখানে লগার, স্কপ এবং রোল তৈরী এবং রোল এসাইন এর একশন তৈরী করা হই
   <details>
     <summary>SettingController-29</summary>
    
     ```c#
    namespace Crud.web.Areas.Admin.Controllers
    {
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<SettingController> _logger;
        public SettingController(ILogger<SettingController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }
        public IActionResult Roles()
        {
            return View();
        }

        public async Task <IActionResult> CreateRole()
        {
            var model = _scope.Resolve<RoleCreateModel>();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
               await model.CreateRole();
            }
           return RedirectToAction(nameof(Roles));
        }
    }
    }

     ```
    </details>
2. View Page Create & CreateRole Action View-25\
    রোল এর ভিউ এবং ক্রিয়েট রোল এর ভিউ থাকবে। 
   <details>
     <summary>CreateRole view</summary>
    
     ```c#
    @model RoleCreateModel
    @{
    ViewData["Title"] = "CreateRole";
    }

    <div class="container-fluid">
    <div class="row">
        <!-- left column -->
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="card card-primary">
                <div class="card-header">
                    <h3 class="card-title">Create User</h3>
                </div>
                <!-- /.card-header -->
                <!-- form start -->
                <form role="form" asp-antiforgery="true" asp-action="CreateRole"
                      asp-area="Admin" asp-controller="Setting" method="post">
                    <div class="card-body">
                        <div asp-validation-summary="All" class="text-danger"></div>
                        <div class="form-group">
                            <label asp-for="Name"></label>
                            <input type="text" class="form-control" asp-for="Name" placeholder="Enter title">
                            <span asp-validation-for="Name" class="text-danger"></span>
                        </div>
                        
                    </div>
                    <!-- /.card-body -->

                    <div class="card-footer">
                        <button type="submit" class="btn btn-primary">Submit</button>
                    </div>
                </form>
            </div>
            <!-- /.card -->

        </div>
        <!--/.col (left) -->
     </div>
     <!-- /.row -->
     </div><!-- /.container-fluid -->
     @section Scripts
    {
    <partial name="_ValidationScriptsPartial" />
     }



     ```
    </details>
3. CreateRoleModel-14\
    এখানে রোল তৈরি কড়ার জন্যে Name একটা প্রপার্টি এবং Rolemanager & usermanager field
    থাকবে। 
   <details>
     <summary>CreateRoleModel-19</summary>
    
     ```c#
    namespace Crud.web.Areas.Admin.Models
    {
    public class CreateRoleModel
    {
        [Required]
        public string Name { get; set; }

        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        
        public CreateRoleModel() { 

        }
        public CreateRoleModel(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        internal void ResolveDependency (ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }

        public async Task  CreateRole()
        {
            if(!string.IsNullOrWhiteSpace(Name))
            {
                await _roleManager.CreateAsync(new ApplicationRole(Name));
            }
        }
     }
     }

     ```
    </details>
4. RoleListModel-14
   <details>
     <summary>Dummy</summary>
    
     ```c#
    
     ```
    </details>
5. Binging WebModule-14
   <details>
     <summary>WebModule</summary>
    
     ```c#
    builder.RegisterType<CreateRoleModel>().AsSelf().InstancePerLifetimeScope();
    builder.RegisterType<RoleListModel>().AsSelf().InstancePerLifetimeScope();
     ```
    </details>
6. Assign Role Controller-37\
    AssignRole আই- একশন বানাতে হবে। এখানে ডাটা তুলে আনার জন্নে Loaddata() method থাকবে।
   <details>
     <summary>AssignRole Action</summary>
    
     ```c#
    //asign role
        public async Task<IActionResult> AssignRole()
        {
            var model = _scope.Resolve<RoleAssignModel>();
            await model.LoadData();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(RoleAssignModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.AssignRole();
            }
            return RedirectToAction(nameof(Roles));
        }
     ```
    </details>
7. RoleAssignModel class-39\
    এখানে list এর মধ্যে role and user তুলে নিয়ে আসবে। Loaddata used করে
    username অনুযায়ী রোল সেট কড়ার জন্য ২ টা প্রপার্টি নিতে হবে। 
   <details>
     <summary>RoleAssignModel</summary>
    
     ```c#
    namespace Crud.web.Areas.Admin.Models
    {
    public class RoleAssignModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string RoleName { get; set; }
        public List<SelectListItem>? Roles { get;private set; }
        public List<SelectListItem>? LUsers { get; private set; }

        private RoleManager<ApplicationRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;
        
        public RoleAssignModel() { 

        }
        public RoleAssignModel(RoleManager<ApplicationRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        internal void ResolveDependency (ILifetimeScope scope)
        {
            _roleManager = scope.Resolve<RoleManager<ApplicationRole>>();
            _userManager = scope.Resolve<UserManager<ApplicationUser>>();
        }
        //used for Load Username and role name in vied page
        internal async Task LoadData()
        {
            LUsers = await (from c in _userManager.Users
                           select new SelectListItem($"{c.FirstName} {c.LastName}", c.UserName))
            .ToListAsync();

            Roles = await (from c in _roleManager.Roles
                           select new SelectListItem(c.Name, c.Name))
                     .ToListAsync();
        }
        //assign role
        internal async Task AssignRole()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(Username);
            await _userManager.AddToRoleAsync(user, RoleName);   
        }

    }
    }
     ```
    </details>
8. AssignRole View-50-14
   <details>
     <summary>AssignRole View </summary>
    
     ```c#
    @model RoleAssignModel
    @{
    ViewData["Title"] = "AssignRole";
    }

    <div class="container-fluid">
    <div class="row">
        <!-- left column -->
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="card card-primary">
                <div class="card-header">
                    <h3 class="card-title">Create User</h3>
                </div>
                <!-- /.card-header -->
                <!-- form start -->
                <form role="form" asp-antiforgery="true" asp-action="AssignRole"
                      asp-area="Admin" asp-controller="Setting" method="post">
                    <div class="card-body">
                        <div asp-validation-summary="All" class="text-danger"></div>
                        <div class="form-group">
                            <label asp-for="Username"></label>
                            <select asp-items="@Model.LUsers"  asp-for="Username" ></select>
                            <span asp-validation-for="Username" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <label asp-for="RoleName"></label>
                            <select asp-items="@Model.Roles" asp-for="RoleName"></select>
                            <span asp-validation-for="RoleName" class="text-danger"></span>
                        </div>

                    </div>
                    <!-- /.card-body -->

                    <div class="card-footer">
                        <button type="submit" class="btn btn-primary">Submit</button>
                    </div>
                </form>
            </div>
            <!-- /.card -->

        </div>
        <!--/.col (left) -->
    </div>
    <!-- /.row -->
    </div><!-- /.container-fluid -->
    @section Scripts
    {
    <partial name="_ValidationScriptsPartial" />
    }

     ```
    </details>
9. Register AssignRole -58
   <details>
     <summary>WebModule</summary>
    
     ```c#
    builder.RegisterType<RoleAssignModel>().AsSelf().InstancePerLifetimeScope();
     ```
    </details>
10. Policy Based Authorization-80\
    একাধিক Role কে একত্র করে একটা পলেছি বেজ authorization তৈরি করা হয়
     পলেছি বেজ এ যে কইটা role অ্যাড করব , কোন ইউজার আআর ওপর  সেই পলেছি অ্যাপ্লাই
     করতে হলে আবসসই পলেছি সব রোলে ইউজার এর মধ্যে অ্যাড করতে হবে। 
     একটা area or controller a  একাধিক পলিচ্য অ্যাড করা যাই  না।
   <details>
     <summary>Code in servicecollectionextention</summary>
    
     ```c#
        //Policy Based Role Management
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ITPerson", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("HR");
                    policy.RequireRole("IT");
                });
            });
     ```
    </details>
11. Configure Claim Based-90/
   <details>
     <summary> add claim option in ServiceCollectionExtention</summary>
    
     ```c#
    //Role Management
            services.AddAuthorization(options =>
            {
                //Policy Based
                options.AddPolicy("ITPerson", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("HR");
                    policy.RequireRole("IT");
                });
                //Claim Based
                options.AddPolicy("UserViewPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("ViewUser", "true");
                });
            });
     ```
    </details>
    <details>
     <summary>Set authorization role for view</summary>
    
     ```c#
    [Authorize(Policy = "UserViewPolicy")]
     ```
    </details>
12. Create AssignClaim IAction -94
   <details>
     <summary>AssignClaim</summary>
    
     ```c#
    //Assign Claim
        public async Task<IActionResult> AssignClaim()
        {
            var model = _scope.Resolve<RoleAssignModel>();
            await model.AsignStaticClaim();
            return View();
        }
     ```
    </details>
    <details>
     <summary>create AsignStaticClaim() in RoleAssignModel</summary>
    
     ```c#
    internal async Task AsignStaticClaim()
        {
            ApplicationUser user = await _userManager.FindByNameAsync("it@crud.com");
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("ViewUser", "true"));
        }
     ```
    </details>
now run project and go to AssignClaim action url. তাহলেই ক্লেয়াম অ্যাড হয়ে যাবে নিদ্দিস্ত ইউজার এ 
claim  ইউজার এবং রোল দুইতাতে দেওয়া যায়। 
13. Alternative option for Claim Based - 104
   <details>
     <summary>add claim option in ServiceCollectionExtention</summary>
    
     ```c#
    //Alternative option for Claim Based
      options.AddPolicy("UserViewRequirementPolicy", policy =>
         {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new UserViewRequirement());
        });
        });
        //part of Alternative option for Claim Based
        services.AddSingleton<IAuthorizationHandler, UserViewRequirementHandler>();
     ```
    </details>
    <details>
     <summary>Add UserViewRequirement method in infrastructure </summary>
    
     ```c#
    namespace Crud.Infrastructure.Securities
    { 
    public class UserViewRequirement : IAuthorizationRequirement
    {
    }
    }
     ```
    </details>
    <details>
     <summary>Add UserViewRequirementHandler method in infrastructure</summary>
    
     ```c#
    namespace Crud.Infrastructure.Securities
    {
    public class UserViewRequirementHandler :
          AuthorizationHandler<UserViewRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               UserViewRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "ViewUser" && x.Value == "true"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
    }

     ```
    </details>
    Note: nameof(method name)-31 if we pass method name as a string try to used nameof 

## Class-34 (Web API)
1. Service-02\
    যখন আমরা কোন সফটওয়্যার থেকে সেবা গ্রহন করি সেটা হছে service like pament gateway service.
    service আবার ২ ব্যবহার করা হয় ১) open(any-one can used) 2) restricted (need to credential)
    Service 2 ধরনের হয় 1) web service 2) windows service/Nativ service
2. Web Service-04\
    Web service ডাটা provide  করার জন্য একটা web application, এটা Presentation , View  Provide করে না। 
    Web service  ওয়েব সারভারে রাখতে হয়। 
3. WEB Api-13\
    Application Programming/Programable Interface. web api হতে হলে একটা service এর অবশ্যই restricted হতে হবে।  
4. Create Dot.net Core Web Api Project-25\
    একটা ওয়েব API প্রোজেক্ট নিতে হবে। প্রোজেক্ট তৈরি করতে গেলে আমরা দেখতে পারব Aditional Information এর মধ্যে minimal Api 
    সিলেক্ট করার একটা অপশন আছে।
    minimal apl হছে কন্ট্রোলার ছাড়া প্রোজেক্ট তৈরি করা। কারন controller  একটা হেভি class । প্রোজেক্ট এর লোড কমানোর জন্য এইটা ব্যবহার করা হয়
    Monolethik Application এ minimal api use  করে তেমন উপকার হয় না microservice application a minimal api ব্যবহার করা ভাল।
5. RSET Api-33\
    Rest api মূলত একটা convention. Controller, Action এর নাম একটা নিদিষ্ট ফরম্যাটে করার কনভেনশন হছে রেস্ট আপিয়াই। 
    web project এর controller এ অ্যাকশান গুলোর রিটার্ন টাইপ IAction না হয়ে IEnamurable হয় কারন web- api এর ভিউ থাকে না। 
6.  Serilog  & Autofac Configure\
    SerilogAsp.Net Core & Autofac প্যাকেজ ইন্সটল করে program.cs  configure korte hobe
    <details>
     <summary>Serilog configure in Program.cs-71</summary>
    
     ```c#
    builder.Host.UseSerilog((hc, lc) => lc //hc== hosting context lc= loging context
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
     );
     ```
    </details>
    <details>
     <summary>Serilog configure in Apsetting.jeson</summary>
    
     ```c#
    "Serilog": {
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "Logs/web-log-.log",
          "rollingInterval": "Day"
        }
      }
     ]
     },
     ```
    </details>
    <details>
     <summary>Autofac configure in Program.cs-77</summary>
    
     ```c#
        // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    //collect MigrationAssembly Path
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    //Autofac configuration Start
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new PersistanceModule(connectionString, migrationAssembly));
        containerBuilder.RegisterModule(new ApplicationModule());
        containerBuilder.RegisterModule(new InfrastructureModule());
        containerBuilder.RegisterModule(new ApiModule());

    });
    //Autofac configuration End
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    //Auto Mapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
     ```
    </details>

7. Create a controller-50\
    একটা controller create হবে এবং logger & scop এর dependency injection করতে হবে। 
    তারপর Get, Post,Put, Delete এই মেথড গুলো তৈরি করতে হবে। এগুলোর কাজ হবে CRUD Operation করা। 
    controller এর route এ একটা ভার্সন নাম্বার দিতে হয়, কারন ভার্সন মেইন্তেইন করেই api url তৈরি করতে হয়। 

   <details>
     <summary>UserController</summary>
    
     ```c#
    namespace Crud.API.Controllers
    {
    [Route("v3/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILifetimeScope scope, ILogger<UsersController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<User> Get() { 
            try
            {
                var model = _scope.Resolve<UserModel>();
                return model.GetUsers();
            }
            catch(Exception ex) {
                _logger.LogError(ex, "Couldn't get courses");
                return null;
            }
        }
        [HttpGet("{id}")]
        public User Get(Guid id) {
            var model = _scope.Resolve<UserModel>();
            return model.GetUser(id);
        }
        //[HttpGet("{name}")]
        //public User Get(string name)
        //{
        //    var model = _scope.Resolve<UserModel>();
        //    return model.GetUser(name);
        //}

        [HttpPost()]
        public IActionResult Post(UserModel model)
        {
            try {
                model.ResolveDepenency(_scope);
                model.CreateUser();
                return Ok();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Couldn't Updete user");
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Put(UserModel model)
        {
            try
            {
                model.ResolveDepenency(_scope);
                model.UpdateUser();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't Updete user");
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var model = _scope.Resolve<UserModel>();
                model.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't delete user");
                return BadRequest();
            }
        }
        }
    }
     ```
    </details>

8. Create a Model Class-53/
    controller এ মেথড গুলা লেখা হইসে সেগুলার ইমপ্লেমেন্ত করতে হবে , dependency resolve করতে হবে। মডেল ক্লাস এর একটা 
    এমটি constractor থাকবে। এবং একটা paramiterized constractor থাকবে যেখানে পারামিতের হিসাবে থাকবে IService

    <details>
     <summary>UserMode Class3</summary>
    
     ```c#
    namespace Crud.API.Models
    {
    public class UserModel
    {
        private IUserService? _userService;
        private IMapper _mapper;

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public UserModel()
        {

        }
        public UserModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public void ResolveDepenency(ILifetimeScope scope)
        {
            _userService = scope.Resolve<IUserService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal IList<User>? GetUsers()
        {
            return _userService?.GetAllUser();
        }
        internal void DeleteUser(Guid id)
        {
            _userService?.DeleteUser(id);
        }
        internal void CreateUser()
        {   
            _userService?.CreateUser(Name, Email, Phone, Address);
        }
        internal void UpdateUser()
        { 
            _userService?.UpdateUser(Id, Name, Email, Phone, Address);
        }
        //internal User GetUser(string name)
        //{
        //    return _userService.GetUser(name);
        //}
        internal User? GetUser(Guid id)
        {
            return _userService?.GetUser(id);
        }

        public async Task<object?> GetPagedUsers(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _userService? .GetPagedUserAsync(
               dataTablesUtility.PageIndex,
               dataTablesUtility.PageSize,
               dataTablesUtility.SearchText,
               dataTablesUtility.GetSortText(new string[] { "Id", "Name", "Email", "Phone", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Email,
                                record.Phone,
                                record.Address,
                                record.Id.ToString(),

                        }
                    ).ToArray()
            };
        } 
     }
     }
     ```
    </details>

9. Create ApiModule class-70\
    model class বাইন্ডিং করার জন্য একটা Module class তৈরি করতে হয় এবং এই ক্লাস টা program.cs a অটো ফেক এর ভিতর বাইন্ডিং করতে হবে।  
    <details>
     <summary>ApiModule</summary>
    
     ```c#
    namespace Crud.API.Models
    {
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserModel>().AsSelf();
            base.Load(builder);
        }
     }
     }
     ```
    </details>
## Class-35 (Web Api-JWT Token)
1. JWT Token-55\
    ইউজার যখন কোন রিকুয়েস্ট পাঠাবে সারভারএ তখন ইউজার কে লগিন করতে হয়, এখন api তে যখন ইউজার রিকুয়েস্ট পাঠাবে
    তখন api HTTP based হওয়াই ইউজার এর ক্রিদেন্তিয়াল ধরে রাখবে না। এই সমসসা সমাধানে জন্য JWT Token ব্যবহার করা হয়।
    এটা একটা টোকেন তৈরি করে প্রতি রিকুয়েস্ট এর সাথে পাঠাই দেই যার কারনে বারবার লগিন করতে হয়ই না।
    সহজ কথাই JWT হছে API request authentication system
2. Create TokenController-59\
    এখানে ইউজার ম্যানেজার, সাইন-ইন ম্যানেজার, আই-কনফিগারাসিওন, টোকেন সার্ভিস থাকে।
    Get method এর মাধমে email, password নিয়ে চেকিং করে, সব ঠিক থাকলে টোকেন সার্ভিস
    এর মাধমে টোকেন জেনেরাট করে স্ট্রিং করে রিটার্ন করে দেই। 

    <details>
     <summary>TokenController</summary>
    
     ```c#
    namespace Crud.API.Controllers
    {
    [Route("v3/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase 
    {  
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public TokenController(IConfiguration config,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _configuration = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string email, string password)
        {
            if (email != null && password != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

                if (result != null && result.Succeeded)
                {
                    var claims = (await _userManager.GetClaimsAsync(user)).ToArray();
                    var token = await _tokenService.GetJwtToken(claims);

                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
    }
     ```
    </details>
3. Create TokenService Class for create Token-60\
    এখানে ইন্টারফেস এবং ক্লাস ২ টাই থাকবে। এইটা infrastructure layer ar security folder এর ভিতর তৈরি করব। 
    এর কাজ হবে টোকেন জেনারেত করা । এবং আপ সেটিং এ key টা দিয়ে দিতে হবে। 
    need package: Microsoft.AspNetCore.Authentication.JwtBearer
    <details>
     <summary>ITokenService</summary>
    
     ```c#
    namespace Crud.Infrastructure.Securities
    {
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims);
    }
    }
     ```
    </details>
    <details>
     <summary>TokenService</summary>
    
     ```c#
    namespace Crud.Infrastructure.Securities
    {
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration config)
        {
            _configuration = config;
        }
        //Used to generate token
        public async Task<string> GetJwtToken(IList<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
    }
     ```
    </details>
    <details>
     <summary>Appsetting.jeson-67 for token key</summary>
    
     ```c#
    "Jwt": {
    "Key": "akjfjskghghjkfskjwjfewjifjksdjfksjfkdsfk",
    "Issuer": "https://localhost:44322",
    "Audience": "https://localhost:44322",
    "ClientId": "Demo",
    "Subject": "AccessToken"
    },
     ```
    </details>
4. Binding Tokenservice in module class-91
    <details>
     <summary>Tokenservice Binding for dependency</summary>
    
     ```c#
    public class InfrastructureModule : Module
    {     
        public InfrastructureModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();

        }
        
    }
     ```
    </details>
5. Add AddIdentity in Program.cs-92\
    Need Package: Microsoft.AspNetCore.Identity.UI
    <details>
     <summary>AddIdentity add in program file</summary>
    
     ```c#
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    //Auto Mapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddIdentity();
     ```
    </details>

    
    <details>
     <summary></summary>
    
     ```c#
    
     ```
    </details>
   