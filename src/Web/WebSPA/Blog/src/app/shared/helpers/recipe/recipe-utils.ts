import { Injectable } from "@angular/core";
import { ActivatedRoute, UrlSegment } from "@angular/router";
import { EDifficulty } from "src/app/core/models/difficulty/difficulty.model";

@Injectable({
    providedIn: 'root'
})
export class RecipeUtils {
    constructor() {

    }

    getIdFromCurrentUrl(url: UrlSegment[]): string {
        if (url.length > 0) {
            const segments = url.map(segment => segment.path);
            var id: string = segments[segments.length - 1];
            return id;
        }
        return '';
    }

    mapDifficulty(difficulty: number): string {
        switch (difficulty) {
            case EDifficulty.Fácil:
                return EDifficulty[EDifficulty.Fácil];
            case EDifficulty.Médio:
                return EDifficulty[EDifficulty.Médio];
            case EDifficulty.Difícil:
                return EDifficulty[EDifficulty.Difícil];
            default:
                return EDifficulty[EDifficulty.Fácil];
        }
    }
}