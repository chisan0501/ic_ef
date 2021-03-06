

    // called when the document completly loaded
    $(document).ready(function () 
    {
             
        var left_printButton = document.getElementById('print_label');
        var printername = document.getElementById('printersSelect');

        function loadPrinters() {
            var non_custom_printersSelect = document.getElementById('printersSelect');

            var printers = dymo.label.framework.getPrinters();
            if (printers.length == 0) {
                alert("No DYMO printers are installed. Install DYMO printers.");
                return;
            }

            for (var i = 0; i < printers.length; i++) {
                var non_custom_printerName = printers[i].name;

                var option = document.createElement('option');
                option.value = non_custom_printerName;
                option.appendChild(document.createTextNode(non_custom_printerName));
                printersSelect.appendChild(option);

            }
        }
       

        loadPrinters();
        // prints the label
        $('body').on('click', '#print_label', function () {
          

            try
            {
                // open label
                var labelXml = '<?xml version="1.0" encoding="utf-8"?>\
<DieCutLabel Version="8.0" Units="twips">\
	<PaperOrientation>Landscape</PaperOrientation>\
	<Id>LargeShipping</Id>\
	<PaperName>30256 Shipping</PaperName>\
	<DrawCommands>\
		<RoundRectangle X="0" Y="0" Width="3331" Height="5715" Rx="270" Ry="270" />\
	</DrawCommands>\
	<ObjectInfo>\
		<ShapeObject>\
			<Name>SHAPE_1</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<ShapeType>Rectangle</ShapeType>\
			<LineWidth>15</LineWidth>\
			<LineAlignment>Center</LineAlignment>\
			<FillColor Alpha="0" Red="255" Green="255" Blue="255" />\
		</ShapeObject>\
		<Bounds X="3128.83056640625" Y="1924.22998046875" Width="2488.90258789063" Height="1244.21508789063" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<BarcodeObject>\
			<Name>BARCODE</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<Text> </Text>\
			<Type>Code128Auto</Type>\
			<Size>Small</Size>\
			<TextPosition>Bottom</TextPosition>\
			<TextFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			<CheckSumFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			<TextEmbedding>None</TextEmbedding>\
			<ECLevel>0</ECLevel>\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<QuietZonesPadding Left="0" Top="0" Right="0" Bottom="0" />\
		</BarcodeObject>\
		<Bounds X="2451" Y="177" Width="3163" Height="600" />\
	</ObjectInfo>\
<ObjectInfo>\
		<TextObject>\
			<Name>channel</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String>Mac</String>\
					<Attributes>\
						<Font Family="Arial" Size="14" Bold="True" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="336" Y="177" Width="1948" Height="456.904602050781" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<BarcodeObject>\
			<Name>asset_tag</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<Text>27221</Text>\
			<Type>Code39</Type>\
			<Size>Small</Size>\
			<TextPosition>Bottom</TextPosition>\
			<TextFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			<CheckSumFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			<TextEmbedding>None</TextEmbedding>\
			<ECLevel>0</ECLevel>\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<QuietZonesPadding Left="0" Top="0" Right="0" Bottom="0" />\
		</BarcodeObject>\
		<Bounds X="4080.80078125" Y="2569.92602539063" Width="1523.79919433594" Height="523.682373046875" />\
	</ObjectInfo>\
<ObjectInfo>\
		<TextObject>\
			<Name>TEXT_2</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String>Asset Tag #</String>\
					<Attributes>\
						<Font Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="4470.28125" Y="2343.4140625" Width="897.529418945313" Height="175.411758422852" />\
	</ObjectInfo>\
<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<Image>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</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="3206.70532226563" Y="1968.29223632813" Width="2343.71435546875" Height="355.763702392578" />\
	</ObjectInfo>\
<ObjectInfo>\
		<TextObject>\
			<Name>refur</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String>Sam</String>\
					<Attributes>\
						<Font Family="Arial" Size="10" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="3606.59985351563" Y="2925.28247070313" Width="1598.51110839844" Height="221.860534667969" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>time</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String>3/17/2015 11:34:42 AM</String>\
					<Attributes>\
						<Font Family="Arial" Size="9" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="823.644409179688" Y="2771.93774414063" Width="2070.541015625" Height="270" />\
	</ObjectInfo>\
<ObjectInfo>\
		<TextObject>\
			<Name>manu</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String>Dell Inc.-OptiPlex 755</String>\
					<Attributes>\
						<Font Family="Arial" Size="12" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="857.01513671875" Y="784.501647949219" Width="4262.19580078125" Height="219.574462890625" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>cpu</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String>Intel(R) Core(TM)2 Duo CPU     E6750  @ 2.66GHz</String>\
					<Attributes>\
						<Font Family="Tahoma" Size="10" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="849.281311035156" Y="1246.90490722656" Width="3438.39965820313" Height="293.936157226563" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>hdd</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String>238 GB</String>\
					<Attributes>\
						<Font Family="Arial" Size="10" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="793.047729492188" Y="2246.919921875" Width="777.765930175781" Height="250.212768554688" />\
	</ObjectInfo>\
<ObjectInfo>\
		<TextObject>\
			<Name>ram</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String>2 GB</String>\
					<Attributes>\
						<Font Family="Tahoma" Size="10" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="854.817504882813" Y="1738.06286621094" Width="621.98779296875" Height="300" />\
	</ObjectInfo>\
<ObjectInfo>\
		<TextObject>\
			<Name>serial</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>ShrinkToFit</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String>3Z2JXF1</String>\
					<Attributes>\
						<Font Family="Arial" Size="10" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0"/>\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="2028.32666015625" Y="1747.0986328125" Width="926.11767578125" Height="256.941162109375" />\
	</ObjectInfo>\
<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC_3</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<Image>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</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="336" Y="571.977783203125" Width="438.666656494141" Height="598.0888671875" />\
	</ObjectInfo>\
<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC_5</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<Image>iVBORw0KGgoAAAANSUhEUgAAAQAAAAEACAYAAABccqhmAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAB3oAAAd6AVXkgXQAABSKSURBVHhe7d19sBbVfQfwc/bykihKMCm+kLExBAGrbeRCQeJLcPLWdGqCYSrFVGeiHZNOWzAdnWbSpK2Dk2hJhbRNk/5RiSFNUlJi2rF1TDr4HsFqx9hyRSMKEUV0RBRB5T67/f52z6OX63Of++zeu2f37O/7mflx9pz7PLvnedjze952z5okSRg5AqZEJvoba6LHEc9ZYzeh7X2dbttrwAyscwPW9wxiF5a/hbZpnW47WkCn/r230217DTgZ6/w+1rcHsRPL30DbOzrdttcA6ec6rG8HYi/6uRFtp3S6ba8B7X4+i3hyPPrZ9LDyD/XGWjvBmr57jUkWuKa2A4mJ34/n8nFX7xnWeTx21p9jcXrW8oZHsM55WOchVx9Vl/7td/170tV7hnWeiP49jMV3Zi1vGHD9e9XVe4Z1TnT9nO+a2mrVTw0iV1JP+n6/w+AS8mp2rVvOBff7KxTDB7+Yg7+udMs9GrF/U7Gdr7rlXHC/v0AxfFCJufjr59xyTtFlHQa/GEs//xLFOPez+ZgAcohMco5bfAu8j1roFnPpdj9rklzrrKB/I26vG+x0I65zDP0csS9F+6kBE0AO+NR0nFvsZJor8+pyvyjXOivoX7ftjQj97NaXov3s0pdi/dSACYBIMSYAIsWYAIgUYwIgUowJgEgxJgAixZgAiBRjAiBSjAmASDEmACLFmACIFGMCIFKMCYBIMSYAIsWYAIgUYwIgUowJgEgxJgAixZgAiBRjAiBSjAmASDEmgHwGXdlJy5V5dblfnHedvvvXbXvddOtL0X526UvhfjYeE0AO1thtbrED+6hbyCkZ8X7YXq51VtC/LtsbWffHVbifA27hLYr2UwMmgBxiE38fxetZ7UiJSTa4xVwSE93kFoeLsb1c6yypfyPdL3f/2lw/D2e1I6GfIz0fXaGf693icIX7qUIy7GKBjO4hl9+yJjqASNoRmeib+EN6ncUigfuvxnpaQ9b5KnLzH3a67WhRUv+ux3qG9u8Q+ve5TrftNdDPS7CeV4asM8Z21nW6ba9RRj+bHrw4aAHW2pOwA5+LxWPwkXULnkO5uOeYYJ2zsc4PYBGvjK27sM7cF8hsc/07D4tTxrF/c7HOs7CIdxitO7HOXdlfisM6Z7h+HoV1/gzr/L/sL8W5fi7GIpJo+jyOuZ9NxgRApBi/AyBSjAmASDEmACLFmACIFGMCIFKMCYBIMSYAIsWYAIgUU30gUGT7JAGeiJgxJE5ATECQDnL24R7EbsTT7TJOWkXPSgyKugSAQX80io8hPoH4bcRxCKKh9iP+E3GzlEgGL0ljE6lJABj4/Si+hPgo4m3SRtQDObvyvxDXIhHck7Y0SOMTAAb+e1Bci/g9hJU2ooJ+hPgCEsH2rBq+xiYADPyJKFYjViEmSRvROJDZheT06quQCF5NWwLWyASAwf8uFP+KkFN2icpwP2IpkoB8aRisxiUADP5fR/FjhLz1JyrTMwhJAluyangadRwABr9MLiFf1HDwkw/yE/Id2O/k16QgNeYdAP4TZqLYiuDPeuTby4iz8E5gzDMa+daIdwAY/Mei+HcEBz9V4RjEv2E/fGdWDUfwCQBPujwGmWV2btpAVI33IjZifwzqKNImvAO4FPFb2SJRpZYgrsgWwxD0dwDItnJE32OId6cNRNXbi3hfnLTke4HaC/0dwB8jOPipTqYjrs4W6y/YdwB49X8Hih2IaWkDUX0cRMi7ADlOoNZCfgdwEYKDn+roKMQl2WK9hZwA5HReoroKYv8M8iMA3v5PQfE8YnLaQFQ/MeIkfAx4NqvWU6jvAORnPw5+qjMZWxdki/UVagL4kCuJ6qz2+2moCeBkVxLVWe3301ATgEzeSVR3td9PmQCIynOCO1eltoL7FcAd/nsoq1VGJoC4FfHfCJkf7lTEfMRHEIsRRG0nxklLph2vpRATwPEoqnpC5QivP0P8Hf5TOz5x6J+cDLIGIT9VEs3BvlLbSURD/AhQ1cy+Mjd8P/4z/3akwS/wt2+heD9iX9pA2tV6JupQvwOowpUY3I+45a5wu8dRyGzERLXGBNCbn2BQ/5Nb7glufxOK27IaUT0xAfRGphgvYpMriWrpiC8BrbVTUZR1hp1cUGE3tjembx0j2ycX7/R9mqV89n/QLfcMfZ2H4oGsRkrN7fWjo8A+I98ZyM/cZU0ttg/9kWsfptIEYO3EJdbEa1E/Q9rSv5RjP1Z+Y2ziL2K78o16bhUkAElYb8eT9lpW7R36KucryE+Wtf4iiErVUwLAviK/Gn0FIacRyyS3ZZH9+WHEKvRrs+yYi6yJZC59jx8Hkk1xEn/KVXKpIAGIU/FkydRjuaCvs1A8mtVIqV4TwC0oPp7VvJCzFT8QWWPl+nmevwuwF+Ljhhw4E4qifQ3pMVJFMPjl4DGfg1/ImF+Nf6oaiFFIg0OuOFRE0fuRLnLcSBXmSxZ4Klv2zVa03UIuR5Ze6JZ7gtsvQHF5ViPqqqq5A5+KEmM2uIpPGPytO9xyCPoQN2FQy1xvo8Lt5Mu/9Qi5H9FoNiOqOLx9A94BxGusMd9zDT48k5h4eZIkQcybPoSc8LMFg1t+2hsR/i5XJ74PcVraQDSKOGm9iGIFQq4p4IuM+TVvHAdgrV2E3XchMkIpxwHEeMnHVrajuA3blAdcCAZYFb8CDCXHM9yA+A/Eg/jPewl9kmvDSWL4KOJPEZMQRKLn4wCwH8m1LT+MkBebst49yjkqW9AneZEK8mzAqhPAUPLkSV/kMtH8rZ86yXUgkG/yJSAVJ4P+JFcSBYcJgEgxJgAixZgAiBTjl4B+PYd4IVuknGQqOLkgbGhq/SUgE4BfX8DO8FW3TDng//2bKGS+xdDwVwAiqicmACLFmACIFGMCIFKMCYBIsaEnAx2Not+Y9ISEMgwa09qG7e1w9UL4K4BOWn4FwOOciWIuoqxJQeVn6AfQp1ekkiYAa/v+xGYTEvZ0vvvYJJsTk1yK7f7SNeTCBKBT0xMAHt8pKORaEmenDeWSCXllX/x6ZO2E5Rj869DgYfALu8Savk14x1FWhiMKCga/nD5+M8LH4Bcy1tdhu8sjDP7PZ20+JfPxDudcVyHS7nyETCTj2+cjDMY5ruJZXNF2iWpHPvdXYQ4SgB1wFc+iirZLVDu5rzkxTgZkUlC5lr1ndqsxg3e6CpF2MinoQ9miV2uiJBnciCSwEpVCl+rKT34FaC1LkqTlGohUi5PWYRRLEXenDeWTsb4S293I4wD84s+ABTX9Z8A2PE7/xwGEhAlAJy0JwDceCkykGBMAkWJMAESKMQEQKcYEQKQYEwCRYkwARIoxARApxgRApBgTAJFiTABEih1xLoC1diqKaVlt3A0idmN7Yzr5gOcC6KToZCCLYgairJOB9qE/+91ye1LQiUusideifoa0pX8px36s/MbYxF/EdgudfswEoFPTEwAe3xQUMjHvJYhjpa0k8gL8MGIV+rU5wqv+Igz+n6JB5iQrc/CLqdj6Kmvsd1ydiDI/QPwRoszBL2SMy1j/KZLOogiDcTUqnr8LsBci8cx3FSLVMBAXo/h4VvNGxvxq/FPVQIyYAIgyv+FK3+ZLFtidLftmK9ouUe3scaVvu2VS0Co+jz9lTOt2t0yknUwKWkUS+A7eAcRrrDHfcw0+PJOYeHmSJC+7OpFqcdJ6EcUKxN60wQ8Z82uGTgq6CJ/LFyIjlHIcQIyXfGxlO4rbsE15wIXwZ0Cdmv4zoMBjlAl5P4w4FdEnbSXYh9iCPt0nFU4K6hcTQEEaEkAV5EtAIlKKCYBIMSYAIsWYAIgUYwIgUowJgEgxJgAixZgAiBRjAiBSjAmASDEmACLFhp4MNM2YCQuMSeSEhDIMGtPahnIA2yx8AgLPBdBJyclA8oI8B3EaoqxJQV9A3I8+yUlB7UlB+662xlyD+mRpLJe9NzGtT2O7T7iGXJgAdGp6AsDjm4ViA+I304ZyvYb4Mvp1fWTthBUY/NehwcPgF8lia6If4h1HWRmOKCgY/JNQbEL4GPxCxvp12O6KCIN/Vdbm1Ty8wznXLRNpdz7i9GzRq1X4zJHIZ44KxBVtl6h2ZrrStzlIAHbAVTyLKtouUe085krfBmRS0DWu4pHdaszgna5CpJ1MCvpQtujVmihJBjciCaxEpdCluvJLNiemtSxJkpZrIFItTlqHUSxF3J02lE/G+kpsd+PQ4wCORtFvTDoxYRnS4wCwvR2uXgh/BtSp6T8DtuFxyvcBcxFlHgfwAPr0ilQ4KahfTAAFaUkAvvFQYCLFmACIFGMCIFKMCYBIMSYAIsWYAIgUYwIgUowJgEgxJgAixY44EtBaOx1FiYcCmyexPSkL45GAOik6FFgOAX4PorRDgdGfvW65PSXYxI9ZE69D/dSsuTQHrTE3xSa+Cts94NpyYQLQqekJAI9vKgo5M/dixNulrUSPIuRkoFsjvOqfjcF/CxrKHvziKLzf+Kw19p9dnYgy/4K4HFH24Bcy1m9B0jk7wmCUyUA9fxdgfweJx9f8Z0S1hoF4DoqPZDVvZMxfg39sf1b3Lapou0S1c4YrfeuXLLArW/bNVrRdotrZ7UrfdsqUYOtdxaedxrRud8tE2smUYE9ni159G+8A4rXWmBtdgw+7EhNflCRJOiMJkXZx0noJxUUIn0lAxvzaoVOCnYnP5ecgI5RyHECcHgdgt+GV/yfY5suuOTf+DKhT038GFHiMx6L4EKLsS4PdhT79j1Q4JZhfTAAFaUgAVZAvAYlIKSYAIsX4EcCvWxF3ZIuU0ycRC7PFoNT6IwATAFG5+B0AEdUTEwCRYkwARIoxARApxgRApBgTAJFiTABEig09GWiaMRMWGJOUOCloaxvKAWyz8MEHPA6AApPnZCB5QZ6DKPtkoPvRp31SSROAtX1XW2NkarDJ0lgue29iWp/Gdp9wDbkwAVBgekoA2K9nodiA8DFV3muIL6Nf12Pc962wJvlu1u7Ng4mJFyIJ5J4inAmAAjNqAsA+PQnFA4jT0wZ/Lo6QAa50FZ/m4R3OeW6ZSLvzEb4Hv7gSnzmS2a7iWVzRdolqZ6YrfZuNBCCz9FQhqmi7RLXzmCt92yaTgn7NVTyyW40ZvMtViLSTSUEfyha9+lqUJIMbkQRWonIwaytbsjkxrWVJkrRcA5FqcdI6jGIp4u60oXwy1uXSYBuHHgdwFIozjJkwLW0Yfxjwg9uxvTFdD4C/AlBgcs0HgP37V1HIpbv60obxJ7//P4w+pS/4nBCEqFycEISI6okJgEgxJgAixZgAiBRjAiBSjAmASLEQE8ABVxKFoNb7a3DHAYjI9snllI/JakS1FSMmx0kr92nvvoT6EWC3K4nqbG+dB79gAiAqT+3301ATwFOuJKqz2u+noSYAnkpMIaj9fhrql4DTUcgJQaEmMNJhVpy0fuGWaynIAYQndS+K+7IaUS1tq/vgFyG/gt7sSqI6CmL/DPIjgMDHgJNRyFxqMqUyUZ3I7/+n4x3AQFatr2DfAeDJlZmFvpHViGplfQiDXwT7DkDgXcC7UDyOODZtIKreIYR8+RfEsSpBf4uOJ/l5FNdnNaJauCGUwS+Cfgcg8C5AJjPdgqjiyipEQz2KmI8E8HJWrb/gf0fHky2zm16AkHcDRFXZj7ggpMEvgk8AAk/6Eyg+hZD51Yl8k2tcLMd+uD2rhqMRCUDgyb8TxWcRYX+moRBdhf3vVrcclOC/Axgusn3LUHwbId8NEJXpdcQVGPzrs2p4GpcABJLAPBQ/Rrw7bSAaf88hlmLw35NVw9SYjwBD4T/lQRQLEPKxgGi83Y9YEPrgF41MAAL/OXsQ52HxQoT8PEM0Vk8iLkYsxL61M20JXCM/AgyHjwQTUPwB4kuIE6WNKAf5ifkriL/HwH8tbWkIFQmgDYlA3vGchfgk4hOIWQiiTuTVXr5HkrP67sLAb+Tl7FUlgOGQEOagOA0h7wracTxC3jGQDjKwZX4JmWBG4mnEdgz4/0XZeKoTAJF2jf0SkIhGxwRApBgTAJFiTABEijEBECnGBECkGBMAkWJMAESKMQEQKcYEQKQYEwCRYkwARIrxZKACItv3KyhkspGpiHvipPWItI8F1nkKinMRMrPxHVhn4YtLuP59ECFXTLob6xrzbLVY50wU5yDkfPjbsU45c25MsE4581KexykI6eeYJ25x/ZTnUa7QI8/jmPvZaJIAGL2HNdHvIl5EJC5ixFpEmkyLBO7754jDiPY6DyIu63Tb0QL369S/v+50214D978GMejWJ3EA8ZlOt+01cP+LEC+59Um0ENd1um2vgfuPez+bHh0bGZ0DO9NsxCG3cw2PKzrdZ7TA/ZYNW087ZEde1Ok+IwVu361/l3e6z2iB+8lA7bQ+SVj9ne4zWuB+v4Z41a1neNSmnxqC3wHkI/PBvS1bfItLXZnXZ1w5XB/ikmyxZ936N9J2RjPS45JJU4o+Zunn5GzxLYr28zJXDjeWfjYeE0A+MnvQSGa7Mq9u98u7Tt/967a9bsbzMbd160vRfjYeE0A+3aYKk1fsIrrdL+86ffev6NRp4/mY27r1pWg/G48JgEgxJgAixZgAiBRjAiBSjAmASDEmACLFmACIFGMCIFKMCYBIMSYAIsWYAIgUYwIgUowJgEgxJgAixZgAiBRjAiBSjAmASDEmACLFmACIFGMCIFKMCYBIMSaAfF5wZScvujKvbvfLu07f/eu2vW7G8zG3detL0X42HhNAPve6spOtrsyr2/3yrtN3/7ptr5vxfMxt3fpStJ+Nx4uD5hDZPplf/meI+WnDmw4gzoyT1i+yau+wzhNQPISYnja8SS442o91HsyqoyupfzNQ/BxxXNrwpgGE9E8uwpkL1jkJhQzK/rThTfsR0s8nsmrvyuinBnwHkAN2okEUSxA3IHYgnkf8CFFocAncbw+KeYjvImT5l4h/RCzG33oe/KKk/slVis9E/ADxLGIX4h8Q0r9Cgwr3ex2FXL346wgZ7M8hfogoNPjFsH7uRexEjKmfzWfM/wPf779BWoni9QAAAABJRU5ErkJggg==</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="336" Y="1022.08892822266" Width="429.288879394531" Height="701.244445800781" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC_7</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<Image>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</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="336" Y="1528.46667480469" Width="429.288879394531" Height="673.111083984375" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC_1</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<Image>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</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="3271.24462890625" Y="2775.68896484375" Width="269.866638183594" Height="429.288879394531" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC_4</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<Image>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</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="336" Y="2062.95556640625" Width="391.777770996094" Height="569.95556640625" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC_6</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<Image>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</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="1584.66674804688" Y="1575.28894042969" Width="354.266662597656" Height="541.822204589844" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC_2</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<Image>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</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="336" Y="2578.68896484375" Width="363.644439697266" Height="551.200012207031" />\
	</ObjectInfo>\
</DieCutLabel>';
                var labelUri = "http://ic.myicdb.org/mac.label";
                var label = dymo.label.framework.openLabelFile(labelUri);
                //var label = dymo.label.framework.openLabelXml(labelXml);
                
                
                //set label text
                //var channel_name = document.getElementById('channel_dropdown');
                
                var product_name = document.getElementById('channel_content').innerHTML;;
                var manu = document.getElementById('model_content').innerHTML;
                var cpu = document.getElementById('cpu_content').innerHTML;
                var ram = document.getElementById('ram_content').innerHTML;
                var hdd = document.getElementById('hdd_content').innerHTML;
                var time = document.getElementById('time_content').innerHTML;
                var serial = document.getElementById('serial_content').innerHTML;
                var asset_tag = document.getElementById('asset_print').value;
                var user = document.getElementById('user_content').innerHTML;;
 
               // label.setObjectText("channel", channel_name);
                label.setObjectText("BARCODE", product_name);
                label.setObjectText("manu", manu);
                label.setObjectText("cpu", cpu);
                label.setObjectText("ram", ram);
                label.setObjectText("hdd", hdd);
                label.setObjectText("time", time);
                label.setObjectText("serial", serial);
                label.setObjectText("asset_tag", asset_tag);
                label.setObjectText("refur", user)
           
              //  label.setObjectText("channel", channel_name.innerText);
  


                
				
                selectedNode = printername.options[printername.selectedIndex];
               label.print(selectedNode.value);
                
              
 
                

            }
            catch(e)
            {
                alert(e.message || e);
            }

      
        }
    )});

    // register onload event
    if (window.addEventListener)
        window.addEventListener("load", onload, false);
    else if (window.attachEvent)
        window.attachEvent("onload", onload);
    else
        window.onload = onload;
 