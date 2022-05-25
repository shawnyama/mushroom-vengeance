using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakableTiles : MonoBehaviour {

    private Tilemap breakableTilemap;

    private void Start() {
        breakableTilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Beam")) {
            SoundManager.PlaySound("brickBreak");
            Vector3 hitPosition = Vector3.zero;
            foreach(ContactPoint2D hit in collision.contacts) {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                breakableTilemap.SetTile(breakableTilemap.WorldToCell(hitPosition), null);
            }
        }
    }
}
