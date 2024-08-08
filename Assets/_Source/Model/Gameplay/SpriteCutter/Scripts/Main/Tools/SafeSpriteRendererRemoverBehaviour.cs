using UnityEngine;
using System.Collections;

namespace UnitySpriteCutter.Tools
{
    internal sealed class SafeSpriteRendererRemoverBehaviour : MonoBehaviour
    {
        static SafeSpriteRendererRemoverBehaviour instance = null;

        internal static SafeSpriteRendererRemoverBehaviour get {
			get {
				if ( instance == null ) {
					GameObject go = new GameObject( "SpriteRendererConverter" );
					GameObject.DontDestroyOnLoad( go );
					instance = go.AddComponent<SafeSpriteRendererRemoverBehaviour>();
				}
				return instance;
			}
		}

        internal delegate void OnFinish();

        internal void RemoveAndWaitOneFrame( SpriteRenderer spriteRenderer, OnFinish onFinish = null ) {
			
			SpriteRenderer duplicatedSpriteRenderer = CreateDuplicatedSpriteRenderer( spriteRenderer );
			
			GameObject gameObject = spriteRenderer.gameObject;
			spriteRenderer.enabled = false;
			SpriteRenderer.Destroy( spriteRenderer );

			StartCoroutine( EndRemovalAfterOneFrame( gameObject, duplicatedSpriteRenderer, onFinish ) );
		}

		SpriteRenderer CreateDuplicatedSpriteRenderer( SpriteRenderer originalSpriteRenderer ) {
			SpriteRenderer result = new GameObject( "temporaryDuplicatedSpriteRenderer" ).AddComponent<SpriteRenderer>();
			result.transform.position = originalSpriteRenderer.transform.position;
			result.transform.rotation = originalSpriteRenderer.transform.rotation;
			result.transform.localScale = originalSpriteRenderer.transform.localScale;

			result.sprite = originalSpriteRenderer.sprite;
			result.color = originalSpriteRenderer.color;
			result.hideFlags = originalSpriteRenderer.hideFlags;
			result.sortingLayerID = originalSpriteRenderer.sortingLayerID;
			result.sortingOrder = originalSpriteRenderer.sortingOrder;
			return result;
		}

		IEnumerator EndRemovalAfterOneFrame( GameObject gameObject, SpriteRenderer duplicatedSpriteRenderer, OnFinish onFinish )
        {
			yield return new WaitForEndOfFrame();

			if ( onFinish != null )
				onFinish();

			Object.Destroy( duplicatedSpriteRenderer.gameObject );
		}
	}

}