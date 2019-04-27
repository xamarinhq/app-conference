using System;
using Xamarin.Forms;
using Conference.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;

[assembly:ExportCell(typeof(ImageCell), typeof(ImageCellEx))]
namespace Conference.Droid
{
    public class ImageCellEx: ImageCellRenderer
    {
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Context context)
        {
            var cell = (LinearLayout)base.GetCellCore(item, convertView, parent, context);

            var image = cell.GetChildAt(0) as ImageView;

            if (image != null)
            {
                var layoutParams = (LinearLayout.LayoutParams)image.LayoutParameters;
                layoutParams.Height = ViewGroup.LayoutParams.WrapContent;
                layoutParams.Width = ViewGroup.LayoutParams.WrapContent;

                var px12 = (int)TypedValue.ApplyDimension(
                             ComplexUnitType.Dip,
                             12f, 
                             context.Resources.DisplayMetrics
                         );

                    var px8 = (int)TypedValue.ApplyDimension(
                        ComplexUnitType.Dip,
                        8f, 
                        context.Resources.DisplayMetrics
                    );

                layoutParams.SetMargins(px12, px8, 0, px8);
                image.SetScaleType(ImageView.ScaleType.FitCenter);
            }

            var layout = cell.GetChildAt(1) as LinearLayout;
            if(layout == null)
                return cell;

            var label = layout.GetChildAt(0) as TextView;
            if (label == null)
                return cell;

            label.SetTextSize(ComplexUnitType.Dip, 16);

            var secondaryLabel = layout.GetChildAt(1) as TextView;
            if (secondaryLabel == null)
                return cell;

            secondaryLabel.SetTextSize(ComplexUnitType.Dip, 13);


            return cell;
        }

    }
}
