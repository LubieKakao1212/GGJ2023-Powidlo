using UnityEngine;

public class UnitActionMove : UnitActionBase {

    [SerializeField] private float sphereCastRadius = 0.5f;
    [SerializeField] private LayerMask sphereCastMask;

    [SerializeField] private int meshResolution;
    [SerializeField] private float animTime = 0.1f;

    [SerializeField] private GameObject pointerObj;
    [SerializeField] private MeshFilter meshFilter;

    private Mesh _mesh;
    private int _animId;

    private int _resolution;
    
    protected override void UpdatePointer(Vector2 pos) {

        pointerObj.transform.position = GetMoveTarget();

    }

    protected override void Perform() {
        
    }

    public override void EnableControl() {
        base.EnableControl();

        if (!_mesh) {
            _mesh = new Mesh();
            _resolution = meshResolution;
            _mesh.FillViewMeshIndices(_resolution);
        }

        var pos = unit.transform.position;
        
        _animId = LeanTween.value(gameObject, 0f, 1f, animTime).setOnUpdate(r => _mesh.FillViewMesh(pos, 5 * r, sphereCastRadius, sphereCastMask, _resolution)).id;
        meshFilter.mesh = _mesh;
    }

    public override void DisabeControl() {
        base.DisabeControl();
        LeanTween.cancel(_animId);
        meshFilter.mesh = null;
    }

    private Vector3 GetMoveTarget() {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.MousePos);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << 8);

        var pointer = hit.point;
        
        var unitPos = unit.transform.position;
        var targetPoint = new Vector3(pointer.x, unitPos.y, pointer.z);
        var deltaDir = targetPoint - unitPos;
        var moveRay = new Ray(unitPos, deltaDir);
        
        var dist = Mathf.Min(deltaDir.magnitude, 5); //TODO replace 5 with unit movement range
        var pos = Util.CastDir(moveRay, dist, sphereCastRadius, sphereCastMask);
        
        return pos;
    }
    
    
}
