using ExpressionTrees.Database;
using ExpressionTrees.Database.Models;
using ExpressionTrees.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionTrees.Tests
{
    public class ExpressionTreeTest : TestBase
    {
        public override string TestName => "ExpressionTreeTest";

        public override void Test(Filter filter)
        {
            using Context context = new Context();
            Start();

            IQueryable<TestEntity> entities = context.TestEntities;

            List<(Method method, string propertyName, object value)> list = new List<(Method method, string propertyName, object value)>();

            (Method method, string propertyName, object value) r1 = (method: Method.LessThanOrEqual, nameof(filter.ID), value: filter.ID);
            //(Method method, string propertyName, object value) r2 = (method: Method.LessThanOrEqual, nameof(filter.BooleanValue), value: filter.BooleanValue);
            //(Method method, string propertyName, object value) r3 = (method: Method.LessThan, nameof(filter.Date), value: filter.Date);
            (Method method, string propertyName, object value) r4 = (method: Method.LessThanOrEqual, nameof(filter.Value), value: filter.Value);

            list.Add(r1);
            //list.Add(r2);
            //list.Add(r3);
            list.Add(r4);

            //entities = Filter(entities, filter);
            entities = Filter(entities, list);

            //Show(entities.ToList());
            entities.ToList();

            Stop();
        }

        IQueryable<T> Filter<T, Y>(IQueryable<T> source, Y filter)
        {
            Type tType = typeof(T);
            Type yType = typeof(Y);

            ParameterExpression tParameter = Expression.Parameter(tType, "t");
            ParameterExpression yParameter = Expression.Parameter(yType, "y");

            Type[] typeArguments = new Type[] { tType };
            PropertyInfo[] properties = yType.GetProperties();
            foreach (var item in properties)
            {
                object value = item.GetValue(filter);
                if (value == null)
                {
                    continue;
                }

                MemberExpression left = Expression.Property(tParameter, item.Name);
                MemberExpression right = Expression.Property(yParameter, item.Name);

                var method = Expression.LessThanOrEqual(left, right);

                LambdaExpression lambdaExpr = Expression.Lambda(method, tParameter);

                MethodCallExpression call = Expression.Call(typeof(Queryable),
                                                            "Where",
                                                            typeArguments,
                                                            source.Expression,
                                                            Expression.Quote(lambdaExpr));

                source = source.Provider.CreateQuery<T>(call);


            }

            return source;
        }

        IQueryable<T> Filter<T>(IQueryable<T> source, IEnumerable<(Method method, string propertyName, object value)> list)
        {
            Type type = typeof(T);
            ParameterExpression parameter = Expression.Parameter(type, "x");

            Type[] typeArguments = new Type[] { type };

            foreach (var item in list)
            {
                object value = item.value;

                if (value == null)
                {
                    continue;
                }

                //UnaryExpression left = Expression.Convert(Expression.Property(parameter, item.propertyName), item.value.GetType());

                MemberExpression left = Expression.Property(parameter, item.propertyName);
                ConstantExpression right = Expression.Constant(value);

                BinaryExpression method = item.method switch
                {
                    Method.Equal => Expression.Equal(left, right),
                    Method.NotEqual => Expression.NotEqual(left, right),
                    Method.LessThan => Expression.LessThan(left, right),
                    Method.LessThanOrEqual => Expression.LessThanOrEqual(left, right),
                    Method.GreaterThan => Expression.GreaterThan(left, right),
                    Method.GreaterThanOrEqual => Expression.GreaterThanOrEqual(left, right),
                    _ => throw new NotImplementedException(),
                };

                LambdaExpression lambdaExpr = Expression.Lambda(method, parameter);
                //expressions.Add(lambdaExpr);
                //Console.WriteLine(lambdaExpr);
                

                MethodCallExpression call = Expression.Call(typeof(Queryable),
                                                            "Where",
                                                            typeArguments,
                                                            source.Expression,
                                                            Expression.Quote(lambdaExpr));

                source = source.Provider.CreateQuery<T>(call);
            }

            return source;
        }
    }
}
