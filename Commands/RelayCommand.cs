using System.Windows.Input;

namespace PRN212.Command;

public class RelayCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    
    private Predicate<object> _CanExecute;

    private Action<object> _Execute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute)
    {
        _CanExecute = canExecute;
        _Execute = execute;
    }

    public bool CanExecute(object? parameter)
    {
        return _CanExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _Execute(parameter);
    }

}