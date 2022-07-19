﻿using Master.Entity.Domain;
using Master.Infra;
using Master.Service.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
                            obj.login,
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
        [Route("api/v1/auth/forgot")]
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
        [Route("api/v1/auth/forgot_confirm")]
        public ActionResult auth_forgot_confirm([FromBody] DtoAuthForgotConfirm obj)
        {
            #region - code - 

            var srv = new SrvAuthForgotConfirm();

            if (!srv.Confirm(network.pgConnection,
                                obj.mobile,
                                obj.codigo))
            {
                return BadRequest(srv.Error);
            }
            
            return Ok();

            #endregion
        }
    }
}