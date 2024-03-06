using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBoard.ViewModels;

namespace WidgetBoard.Tests.ViewModels;

public class BoardDetailsPageViewModelTests
{
    [Fact]
    public void SaveCommandCanExecuteWithBoardName()
    {
        var viewModel = new BoardDetailsPageViewModel(null, null);

        viewModel.BoardName = "Work";

        Assert.True(viewModel.SaveCommand.CanExecute(null));
    }

    [Fact]
    public void SaveCommandCannotExecuteWithoutBoardName()
    {
        var viewModel = new BoardDetailsPageViewModel(null, null);

        Assert.Null(viewModel.BoardName);
        Assert.False(viewModel.SaveCommand.CanExecute(null));
    }

    [Fact]
    public void SettingBoardNameShouldRaisePropertyChanged()
    {
        var invoked = false;
        var viewModel = new BoardDetailsPageViewModel(null, null);

        viewModel.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName.Equals(nameof(BoardDetailsPageViewModel.BoardName)))
            {
                invoked = true;
            }
        };

        viewModel.BoardName = "Work";

        Assert.True(invoked);
    }
}
