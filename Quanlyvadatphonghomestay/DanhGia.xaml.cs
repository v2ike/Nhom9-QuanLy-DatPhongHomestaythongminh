using Microsoft.Maui.Controls;
using System.Diagnostics.Metrics;

namespace Quanlyvadatphonghomestay; // Hãy đổi lại namespace đúng với dự án của bạn

public partial class DanhGia : ContentPage
{
    public DanhGia()
    {
        InitializeComponent();

        // Trạng thái mặc định khi mở màn hình
        SetState("new");
    }

    // Đếm ký tự cho phần Trải nghiệm
    private void OnReviewTextChanged(object sender, TextChangedEventArgs e)
    {
        int count = e.NewTextValue?.Length ?? 0;
        CounterA.Text = $"{count}/500";
    }

    // Đếm ký tự cho phần Góp ý
    private void OnFeedbackTextChanged(object sender, TextChangedEventArgs e)
    {
        int count = e.NewTextValue?.Length ?? 0;
        CounterB.Text = $"{count}/500";
    }

    // Xử lý sự kiện click trên Tabs mô phỏng
    private void OnTabClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.CommandParameter is string state)
        {
            SetState(state);
        }
    }

    // Hàm chuyển đổi UI theo trạng thái giống script Javascript
    private void SetState(string state)
    {
        // Reset màu tab
        ResetTabs();

        // Ẩn tất cả views trước
        FormContainer.IsVisible = false;
        SubmittedContainer.IsVisible = false;
        EditActions.IsVisible = false;
        SuccessModal.IsVisible = false;

        switch (state)
        {
            case "new":
                SetActiveTab(TabNew);
                FormContainer.IsVisible = true;
                BottomBar.IsVisible = true;
                ReviewEditor.Text = string.Empty;
                FeedbackEditor.Text = string.Empty;
                break;

            case "submitted":
                SetActiveTab(TabSubmitted);
                SubmittedContainer.IsVisible = true;
                BottomBar.IsVisible = false;
                break;

            case "edit":
                SetActiveTab(TabEdit);
                FormContainer.IsVisible = true;
                EditActions.IsVisible = true;
                BottomBar.IsVisible = false;
                // Đổ data mẫu vào để sửa
                ReviewEditor.Text = "Kỳ nghỉ rất tuyệt vời, phòng ốc sạch sẽ, thoáng mát và view biển cực kỳ đẹp lúc bình minh. Gia đình chủ nhà tiếp đón rất nhiệt tình và chu đáo.";
                FeedbackEditor.Text = "Homestay nên trang bị thêm máy sấy tóc công suất lớn hơn và tăng cường thêm đèn đọc sách đầu giường.";
                break;

            case "success":
                SetActiveTab(TabSuccess);
                SuccessModal.IsVisible = true;
                break;
        }
    }

    // Cập nhật giao diện Tab đang Active
    private void SetActiveTab(Button activeBtn)
    {
        // Try to read a 'Primary' color from the merged dictionaries; fall back to a hardcoded color
        var merged = Application.Current?.Resources?.MergedDictionaries?.FirstOrDefault();
        Color bgColor;
        if (merged is not null && merged.TryGetValue("Primary", out var primaryObj) && primaryObj is Color primaryColor)
        {
            bgColor = primaryColor;
        }
        else
        {
            bgColor = Color.FromArgb("#6f4627");
        }

        activeBtn.BackgroundColor = bgColor;
        activeBtn.TextColor = Colors.White;
    }

    // Reset giao diện các Tab về trạng thái Inactive
    private void ResetTabs()
    {
        var inactiveBg = Colors.Transparent;
        var inactiveText = Color.FromArgb("#51443c"); // Tương ứng OnSurfaceVariant

        TabNew.BackgroundColor = inactiveBg; TabNew.TextColor = inactiveText;
        TabSubmitted.BackgroundColor = inactiveBg; TabSubmitted.TextColor = inactiveText;
        TabEdit.BackgroundColor = inactiveBg; TabEdit.TextColor = inactiveText;
        TabSuccess.BackgroundColor = inactiveBg; TabSuccess.TextColor = inactiveText;
    }

    // --- Các Action Buttons ---

    private void OnBackClicked(object sender, EventArgs e)
    {
        // Shell.Current.GoToAsync(".."); 
        // Hoặc Navigation.PopAsync();
    }

    private void OnSubmitClicked(object sender, EventArgs e)
    {
        SetState("success");
    }

    private void OnCancelEditClicked(object sender, EventArgs e)
    {
        SetState("submitted");
    }

    private void OnSaveEditClicked(object sender, EventArgs e)
    {
        SetState("success");
    }

    private void OnEditFeedbackClicked(object sender, EventArgs e)
    {
        SetState("edit");
    }

    private async void OnDeleteFeedbackClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Xác nhận", "Bạn có chắc chắn muốn xóa phản hồi này?", "Xóa", "Hủy");
        if (answer)
        {
            SetState("new");
        }
    }

    private void OnViewSubmittedClicked(object sender, EventArgs e)
    {
        SetState("submitted");
    }

    private void OnGoHomeClicked(object sender, EventArgs e)
    {
        // SuccessModal.IsVisible = false;
        // Thực hiện logic chuyển trang về trang chủ hoặc đóng trang hiện tại
        // Navigation.PopAsync();
    }
}