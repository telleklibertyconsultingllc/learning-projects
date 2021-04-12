export interface TextLayer {
    text: string;
}

export interface ImageLayer {
    image: string;
}

export interface LayerType {
    layer: string;
    text: string;
}

type FactoryLayer<T> = T extends LayerType.text ? TextLayer : ImageLayer;

export function createLayer<T extends LayerType>(layer: T): FactoryLayer<T> {
    if (layer.text) {
        return <FactoryLayer<T>>{
            text: 'download'
        };
    }
    return <FactoryLayer<T>>{
        image: 'img.jpg'
    };
}
