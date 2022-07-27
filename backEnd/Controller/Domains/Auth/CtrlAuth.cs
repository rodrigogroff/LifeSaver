using Master.Entity.Database;
using Master.Entity.Domain;
using Master.Entity.Infra;
using Master.Service.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Api.Master.Controllers
{
    public partial class CtrlAuth : MasterController
    {
        public CtrlAuth(IOptions<LocalNetwork> _network) : base(_network) { }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/auth/login")]
        public ActionResult auth_login([FromBody] DtoAuthLogin obj)
        {
            #region - code - 

            var srv = new SrvAuthLogin();

            DtoUser usr;

            if (!srv.Login(network.pgConnection,
                            obj.email,
                            obj.password,
                            out usr))
            {
                return BadRequest(srv.Error);
            }

            var token = ComposeTokenForSession(usr);

            return Ok(new DtoToken
            {
                token = token,
            });

            #endregion
        }

#if DEBUG

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/auth/magic_sms_list")]
        public ActionResult magic_sms_list([FromBody] DtoAuthSmsList obj)
        {
            #region - code - 

            if (obj.magic != "142536")
                return BadRequest();

            var srv = new SrvAuthMagicSmsList();
            DtoAuthSmsListRet ret;

            if (!srv.List(network.pgConnection, obj.mobile, out ret))
            {
                return BadRequest(srv.Error);
            }

            return Ok(new
            {
                ret = ret.codes[0]
            });
            
            #endregion
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/auth/magic_clean_db")]
        public ActionResult magic_clean_db([FromBody] DtoAuthCleanDB obj)
        {
            #region - code - 

            if (obj.magic != "142536")
                return BadRequest();

            var srv = new SrvAuthMagicCleanDB();

            srv.Clean(network.pgConnection);

            return Ok();

            #endregion
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/auth/magic_user_list")]
        public ActionResult magic_user_list([FromBody] DtoAuthUserList obj)
        {
            #region - code - 

            if (obj.magic != "142536")
                return BadRequest();

            var srv = new SrvAuthMagicUserList();

            List<User> lst;

            srv.UserList(network.pgConnection, out lst);

            return Ok(new
            {
                results = lst,
            });

            #endregion
        }

#endif

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/auth/register")]
        public ActionResult auth_register([FromBody] DtoAuthRegister obj)
        {
            #region - code - 

            var srv = new SrvAuthRegister();

            if (!srv.Register(network.pgConnection,
                                obj.name,
                                obj.email,
                                obj.mobile,
                                obj.password,
                                network.codeExpirationMinutes))
            {
                return BadRequest(srv.Error);
            }

            return Ok();

            #endregion
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/auth/register_confirm")]
        public ActionResult auth_register_confirm([FromBody] DtoAuthRegisterConfirm obj)
        {
            #region - code - 

            var srv = new SrvAuthRegisterConfirm();

            if (!srv.Confirm(network.pgConnection,
                                obj.mobile,
                                obj.code))
            {
                return BadRequest(srv.Error);
            }

            return Ok();

            #endregion
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/auth/resend_code_pass")]
        public ActionResult auth_resend_code_pass([FromBody] DtoAuthResendCode obj)
        {
            #region - code - 

            var srv = new SrvAuthResendCodePass();

            if (!srv.ResendCodeMobile(network.pgConnection,
                                        obj.mobile,
                                        network.codeExpirationMinutes))
            {
                return BadRequest(srv.Error);
            }
            
            return Ok();

            #endregion
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/auth/resend_code_register")]
        public ActionResult auth_resend_code_register([FromBody] DtoAuthResendCode obj)
        {
            #region - code - 

            var srv = new SrvAuthResendCodeRegister();

            if (!srv.ResendCodeMobile(network.pgConnection,
                                        obj.mobile,
                                        network.codeExpirationMinutes))
            {
                return BadRequest(srv.Error);
            }

            return Ok();

            #endregion
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/auth/forgot_pass")]
        public ActionResult auth_forgot([FromBody] DtoAuthForgot obj)
        {
            #region - code - 

            var srv = new SrvAuthForgot();
            
            if (!srv.ForgotPasswordMobile(network.pgConnection,
                                            obj.mobile,
                                            network.codeExpirationMinutes))
            {
                return BadRequest(srv.Error);
            }
            
            return Ok();

            #endregion
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/auth/forgot_pass_confirm")]
        public ActionResult auth_forgot_confirm([FromBody] DtoAuthForgotConfirm obj)
        {
            #region - code - 

            var srv = new SrvAuthForgotConfirm();

            if (!srv.Confirm(network.pgConnection,
                                obj.mobile,
                                obj.codigo,
                                obj.password))
            {
                return BadRequest(srv.Error);
            }
            
            return Ok();

            #endregion
        }
    }
}
