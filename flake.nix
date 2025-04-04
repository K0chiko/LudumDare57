{
  inputs = {
    nix-vscode-extensions = {
      url = "github:nix-community/nix-vscode-extensions";
      inputs.nixpkgs.follows = "nixpkgs";
    };
    nixpkgs.url = "github:NixOS/nixpkgs/nixpkgs-unstable";
  };

  outputs = { self, nix-vscode-extensions, nixpkgs }:
    let
      supportedSystems = [
        "x86_64-linux"
        "x86_64-darwin"
        "aarch64-linux"
        "aarch64-darwin"
      ];
      forAllSystems = nixpkgs.lib.genAttrs supportedSystems;
      pkgsFor = system: import nixpkgs {
        inherit system;
        overlays = [
          nix-vscode-extensions.overlays.default
        ];
      };
    in
    {
      devShells = forAllSystems (system:
        let pkgs = pkgsFor system;
        in
        {
          default = pkgs.mkShell {
            packages = [
              pkgs.dotnet-sdk_9
              pkgs.unityhub
              (pkgs.vscode-with-extensions.override rec {
                vscode = pkgs.vscodium;
                vscodeExtensions = with (pkgs.forVSCodeVersion vscode.version).vscode-marketplace; [
                  jnoortheen.nix-ide
                  ms-dotnettools.csharp
                  (ms-dotnettools.csdevkit.overrideAttrs {
                    postPatch = ''
                      ext_unique_id="$(basename "$out" | head -c 32)"

                      substituteInPlace dist/extension.js \
                        --replace-fail 'e.extensionPath,"cache"' 'require("os").tmpdir(),"'"$ext_unique_id"'"'

                      chmod +x components/vs-green-server/platforms/linux-x64/node_modules/@microsoft/visualstudio-code-servicehost.linux-x64/Microsoft.VisualStudio.Code.ServiceHost components/vs-green-server/platforms/linux-x64/node_modules/@microsoft/visualstudio-reliability-monitor.linux-x64/Microsoft.VisualStudio.Reliability.Monitor components/vs-green-server/platforms/linux-x64/node_modules/@microsoft/visualstudio-server.linux-x64/Microsoft.VisualStudio.Code.Server
                    '';
                  })
                  (ms-dotnettools.vscode-dotnet-runtime.overrideAttrs {
                    patchPhase = ''
                      find "dist/install scripts" -exec chmod +x {} \;
                    '';
                  })
                  shardulm94.trailing-spaces
                  visualstudiotoolsforunity.vstuc
                ];
              })
            ];
          };
        });
      legacyPackages = forAllSystems pkgsFor;
    };
}
