using iFanfics.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace iFanfics.DAL.Interfaces {
    public interface IElasticRepository {
        ResultSetFromElastic GetFanficsFromBody(object body);
        //List<CategoryElastic> GetCategoriesFromBody(object body);
    }
}