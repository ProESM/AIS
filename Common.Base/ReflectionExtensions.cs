﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base
{
    public static class ReflectionExtensions
    {
        public static T GetAttribute<T>(this MemberInfo member, bool isRequired)
            where T : Attribute
        {
            var attribute = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

            if (attribute == null && isRequired)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The {0} attribute must be defined on member {1}",
                        typeof(T).Name,
                        member.Name));
            }

            return (T)attribute;
        }

        public static string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            var displayNameAttr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            if (displayNameAttr == null)
            {
                var displayAttr = memberInfo.GetAttribute<DisplayAttribute>(false);
                if (displayAttr == null)
                {
                    return memberInfo.Name;
                }
                return displayAttr.Name;
            }

            return displayNameAttr.DisplayName;
        }

        public static string GetPropertyDisplayName(Expression<Func<Type, object>> propertyExpression)
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            var displayNameAttr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            if (displayNameAttr == null)
            {
                var displayAttr = memberInfo.GetAttribute<DisplayAttribute>(false);
                if (displayAttr == null)
                {
                    return memberInfo.Name;
                }
                return displayAttr.Name;
            }

            return displayNameAttr.DisplayName;
        }

        public static MemberInfo GetPropertyInformation(Expression propertyExpression)
        {
            Debug.Assert(propertyExpression != null, "propertyExpression != null");
            MemberExpression memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }

        public static string GetFieldDisplayName<T>(Expression<Func<T, object>> fieldExpression)
        {
            var memberInfo = GetFieldInformation(fieldExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No field reference expression was found.",
                    "fieldExpression");
            }

            var displayNameAttr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            if (displayNameAttr == null)
            {
                var displayAttr = memberInfo.GetAttribute<DisplayAttribute>(false);
                if (displayAttr == null)
                {
                    return memberInfo.Name;
                }
                return displayAttr.Name;
            }

            return displayNameAttr.DisplayName;
        }

        public static string GetFieldDisplayName(Expression<Func<Type, object>> fieldExpression)
        {
            var memberInfo = GetFieldInformation(fieldExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No field reference expression was found.",
                    "fieldExpression");
            }

            var displayNameAttr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
            if (displayNameAttr == null)
            {
                var displayAttr = memberInfo.GetAttribute<DisplayAttribute>(false);
                if (displayAttr == null)
                {
                    return memberInfo.Name;
                }
                return displayAttr.Name;
            }

            return displayNameAttr.DisplayName;
        }

        public static MemberInfo GetFieldInformation(Expression fieldExpression)
        {
            Debug.Assert(fieldExpression != null, "fieldExpression != null");
            MemberExpression memberExpr = fieldExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = fieldExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Field)
            {
                return memberExpr.Member;
            }

            return null;
        }
    }
}
