using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace pde_cs
{
    public partial class graph : Form
    {
        public graph()
        {
            InitializeComponent();
        }

        public void DrawGraph(double[] X, double[] Y, Color LColor)
        {
            // Получим панель для рисования
            GraphPane pane = zedGraph.GraphPane;
            pane.Title.Text = "";
            pane.XAxis.Title.Text = "";
            pane.YAxis.Title.Text = "";

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            //  pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list = new PointPairList();

            // Заполняем список точек

            list.Add(X, Y);

            // Создадим кривую с названием "Sinc", 
            // которая будет рисоваться голубым цветом (Color.Blue),
            // Опорные точки выделяться не будут (SymbolType.None)
            LineItem myCurve = pane.AddCurve("", list, LColor, SymbolType.None);

            // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
            // В противном случае на рисунке будет показана только часть графика, 
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraph.AxisChange();

            // Обновляем график
            zedGraph.Invalidate();
        }
    }

   
}
