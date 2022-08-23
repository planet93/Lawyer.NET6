using Lawyer.NET6.BLL.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawyer.NET6.BLL.Service
{
    public class LawyerService
    {
        private EmailService _emailService;
        private IConfiguration _cfg;
        public LawyerService(IConfiguration cfg, EmailService emailService)
        {
            _emailService = emailService;
            _cfg = cfg;
        }

        public async Task<ResultViewModel> Check_and_Send_Feedback(EmailViewModel model)
        {
            ResultViewModel res = new ResultViewModel();

            if(model == null)
            {
                res.Error = true;
                res.ErrorMessage = "Значение модели равно нулю";
                return res;
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                res.Error = true;
                res.ErrorMessage = "Не указана почта";
                return res;
            }
            if(string.IsNullOrWhiteSpace(model.Text))
            {
                res.Error = true;
                res.ErrorMessage = "Не указан текст";
                return res;
            }
            if(model.Text != null && model.Text.Length < 5)
            {
                res.Error = true;
                res.ErrorMessage = "Текст письма должен содержать более 5 символов";
                return res;
            }
            if (!IsValidEmail(model.Email))
            {
                res.Error = true;
                res.ErrorMessage = "Указанная почта не существует";
                return res;
            }

            await _emailService.SendMessage(model.Email, model.Email, "Форма обратной связи", model.Text);

            res.ErrorMessage = "Письмо успешно отправлено";

            return res;
        }

        #region Util

        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        #endregion

    }
}
