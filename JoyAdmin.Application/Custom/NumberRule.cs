using System;
using System.Linq;
using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public interface INumberRule
{
    string GetNextSn(string formCode);
}

public class NumberRule : ServiceBase<Base_NumberRule>, INumberRule
{
    private readonly IRepository<Base_NumberRule> _repository;

    public NumberRule(IRepository<Base_NumberRule> repository) : base(repository)
    {
        _repository = repository;
    }


    public string GetNextSn(string formCode)
    {
        Base_NumberRule numberRule = _repository.Where(x => x.FormCode == formCode)
            .OrderByDescending(x => x.CreateDate)
            .FirstOrDefault();

        if (numberRule != null)
        {
            var lastSn = numberRule.GenerativeRule;
            string rule = numberRule.Prefix + DateTime.Now.ToString(numberRule.DateRule.Replace("hh", "HH"));
            if (string.IsNullOrEmpty(lastSn))
            {
                rule += "1".PadLeft(numberRule.SNLength, '0');
            }
            else
            {
                // 如果日期规则不是当前日期，重置流水号
                if (lastSn.Substring(numberRule.Prefix.Length, numberRule.DateRule.Length) != DateTime.Now.ToString(numberRule.DateRule))
                {
                    rule += "1".PadLeft(numberRule.SNLength, '0');
                }
                else
                {
                    rule += (int.Parse(lastSn.Substring(lastSn.Length - numberRule.SNLength)) + 1).ToString(
                        "0".PadLeft(numberRule.SNLength, '0'));   
                }
            }
            numberRule.GenerativeRule = rule;
            _repository.UpdateNow(numberRule);
            return rule;
        }

        //如果自定义序号配置项不存在，创建一条记录
        var newNumberRule = new Base_NumberRule
        {
            FormCode = formCode,
            Prefix = formCode,
            DateRule = "yyyyMMdd",
            SNLength = 4,
            GenerativeRule =formCode+DateTime.Now.ToString("yyyyMMdd") + "0001"
        };
        _repository.InsertNow(newNumberRule);
        return newNumberRule.GenerativeRule;
    }
}