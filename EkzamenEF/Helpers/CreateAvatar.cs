using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes; 

namespace EkzamenEF.Helpers
{
    public static class CreateAvatar
    {
       
        public static StackPanel CreateAvatarWithoutStatus(Byte[] img, int x, int y,bool Hand)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            Ellipse avatar = new Ellipse();
            avatar.Height = y;
            avatar.Width = x;
            if(Hand)
            avatar.Cursor = Cursors.Hand;

            var brush = ImageConverter.ToImageBrushConvert(img);
            avatar.Fill = brush; 
            stackPanel.Children.Add(avatar);
            return stackPanel;
        }
    }
}
