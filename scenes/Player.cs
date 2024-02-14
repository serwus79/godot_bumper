using Godot;

public partial class Player : Area2D
{
    private bool _dragging;
    private Vector2 _dragOffset;
    private Vector2 _screenSize;
    private Rect2 _field;
    private CollisionShape2D _collisionShape;
    private float _collisionRadius;
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        _screenSize = GetViewportRect().Size;
        if (_collisionShape.Shape is CircleShape2D circleShape)
        {
            _collisionRadius = circleShape.Radius;
        }
        Hide();
        GD.Print("Player jest gotowy");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            if (mouseEvent.Pressed && IsClickInCollisionShape(mouseEvent.GlobalPosition))
            {
                _dragging = true;
                _dragOffset = mouseEvent.GlobalPosition - GlobalPosition;
                GD.Print("Player: Rozpoczęto przeciąganie na pozycji: " + GlobalPosition);
            }
            else
            {
                if (_dragging)
                {
                    GD.Print("Player: Zakończono przeciąganie na pozycji: " + GlobalPosition);
                }
                _dragging = false;
            }
        }

        if (_dragging && @event is InputEventMouseMotion mouseMotion)
        {
            
            var position = mouseMotion.GlobalPosition - _dragOffset;
            // if (_field.HasPoint(position))
            // {
            //     GlobalPosition = position;
            // }
            // var x = _field;
            position = position with
            {
                X = Mathf.Clamp(position.X, _minX, _maxX),
                Y = Mathf.Clamp(position.Y, _minY, _maxY)
            };
            GlobalPosition = position;
            GD.Print("Przeciąganie: Pozycja = " + position);
        }
    }

    private bool IsClickInCollisionShape(Vector2 point)
    {
        if (_collisionShape == null || _collisionShape.Shape == null)
        {
            return false;
        }
        
        var globalPoint = ToGlobal(point); // Przekształca punkt na współrzędne globalne
        var shapeTransform = new Transform2D(0, globalPoint - GlobalPosition);

        // Sprawdzenie kolizji
        if (_collisionShape.Shape.Collide(_collisionShape.GlobalTransform, new RectangleShape2D(), shapeTransform))
        {
            return true;
        }

        return false;
    }

    public void Start(Vector2 startPositionPosition, Rect2 playerFieldRect)
    {
        
        _field = new Rect2(
            playerFieldRect.Position + new Vector2(_collisionRadius, _collisionRadius),
            playerFieldRect.Size - new Vector2(_collisionRadius * 2, _collisionRadius * 2)
        );

        _minX = _field.Position.X;
        _maxX = _field.Position.X + _field.Size.X;
        _minY = _field.Position.Y;
        _maxY = _field.Position.Y + _field.Size.Y;
        
        

        GD.Print("Player: field = " + _field + " ("+ _minX + ":" + _maxX + ") (" + _minY + ":" + _maxY +")");
        GlobalPosition = startPositionPosition;
        Show();
    }
}