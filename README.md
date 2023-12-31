# Avatar Tinker Vista

たまに役に立つ小さい単機能アバターツールの詰め合わせです。

# 特徴
 
- 単純
  - 使えます
  - 設計を凝らず、素早くシンプルに実装されます
- 単機能:
  - 必須な依存性は最低限に絞ってあります。Unity単体で動く機能はVRCSDK不要で動きます
  - フレームワークではない、単機能ツールの寄せ集めです
- 短期サポート:
  - 長期的なサポートはありません
  - 依存ツールのアップデートで動かなくなる可能性はありますが、プロジェクトを壊すことはないようになっています

# 収録ツール

「Window」 → 「Avatar Tinker」 に入っています。

## Replace Material Texture

マテリアルからテクスチャへの参照を一括で置き換えます。

# 収録ndmfツール

「Component」 → 「Avatar Tinker Vista」 に入っています。

## ATiV Select Platform

現在ビルド中のアバターとは異なるプラットフォームのコンポーネントを全て削除します。

## ATiV Delete All PhysBones

PhysBoneを全て削除します。

## ATiV Default VRM0+1 FirstPerson

VRMFirstPerson / Vrm10Instance のFirstPerson設定が行われていないRendererに対して指定されたデフォルト値を割り当てます。

## ATiV Overwrite VRM0+1 Meta

VRMMeta / Vrm10Instance に含まれるメタデータの一部を上書きします。

# 収録ndmfツール (AAO拡張)

## ATiV AAO Merge Other Skinned Mesh

Merge Skinned Mesh に指定されていない全てのメッシュを含む Merge Skinned Mesh を作成します。
