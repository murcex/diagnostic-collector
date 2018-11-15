using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor
{
    public static class GetArticles
    {
        public static void Execute()
        {
            Capsule.Articles = DataDownload.GetArticle();
        }
    }
}
