using UnityEngine;
using UnityEngine.PlayerLoop;

public class UnitActionMove : UnitActionBase {

    [SerializeField] private float sphereCastRadius = 0.5f;
    [SerializeField] private LayerMask sphereCastMask;
    [SerializeField] private float moveTime = 0.25f;
    
    [SerializeField] private int meshResolution;
    [SerializeField] private float meshAnimTime = 0.1f;
    
    [SerializeField] private GameObject pointerObj;
    [SerializeField] private MeshFilter meshFilter;

    private Mesh _mesh;
    private int _animId;

    private int _resolution;
    
    protected override void UpdatePointer(Vector2 pos) {
        pointerObj.transform.position = GetMoveTarget();
    }

    protected override void Perform() {
        var startPos = unit.transform.position;
        var target = GetMoveTarget();
        var sourceMove = unit.MovementLeftThisTurn;
        unit.OnMove(target - startPos);
        LeanTween.move(unit.gameObject, target, moveTime).setEaseInOutQuad().setOnUpdate((Vector3 pos) => UpdateMesh(sourceMove - Vector3.Distance(startPos, pos)));
    }

    public override void EnableControl() {
        base.EnableControl();

        if (!_mesh) {
            _mesh = new Mesh();
            _resolution = meshResolution;
            _mesh.FillViewMeshIndices(_resolution);
        }

        var pos = unit.transform.position;
        var radius = unit.MovementLeftThisTurn;
        
        _animId = LeanTween.value(gameObject, 0f, 1f, meshAnimTime).setOnUpdate(r => UpdateMesh(r * radius)).id;
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
        
        var dist = Mathf.Min(deltaDir.magnitude, unit.MovementLeftThisTurn);
        var pos = Util.CastDir(moveRay, dist, sphereCastRadius, sphereCastMask);
        
        return pos;
    }
    
    private void UpdateMesh(float radius) {
        _mesh.FillViewMesh(unit.transform.position, radius, sphereCastRadius, sphereCastMask, _resolution);
    }
    
}
