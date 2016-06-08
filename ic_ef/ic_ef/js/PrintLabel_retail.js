

    // called when the document completly loaded
    $(document).ready(function () 
    {
             
        var left_printButton = document.getElementById('non_custom_printButton');
        var printername = document.getElementById('non_custom_printersSelect');

        function loadPrinters() {
            var non_custom_printersSelect = document.getElementById('non_custom_printersSelect');

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
                non_custom_printersSelect.appendChild(option);

            }
        }
       

        loadPrinters();
        // prints the label
        $('body').on('click', '#non_custom_printButton', function () {
          

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
		<Bounds X="3900.42797851563" Y="2780.27587890625" Width="1500.24572753906" Height="468.724243164063" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<TextObject>\
			<Name>TEXT_2</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
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
		<Bounds X="4274.001953125" Y="2571.09790039063" Width="897.529418945313" Height="175.411758422852" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<Image>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</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="336" Y="103.190368652344" Width="2787.31176757813" Height="496.975433349609" />\
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
		<Bounds X="793.047729492188" Y="2246.919921875" Width="1683.52209472656" Height="250.212768554688" />\
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
		<Bounds X="854.817504882813" Y="1738.06286621094" Width="4214.06640625" Height="377.419342041016" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC_3</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
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
			<IsVariable>False</IsVariable>\
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
			<IsVariable>False</IsVariable>\
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
			<Name>GRAPHIC_4</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
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
		<TextObject>\
			<Name>price</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>True</IsVariable>\
			<HorizontalAlignment>Left</HorizontalAlignment>\
			<VerticalAlignment>Top</VerticalAlignment>\
			<TextFitMode>None</TextFitMode>\
			<UseFullFontHeight>True</UseFullFontHeight>\
			<Verticalized>False</Verticalized>\
			<StyledText>\
				<Element>\
					<String>1</String>\
					<Attributes>\
						<Font Family="Arial" Size="22" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="4021.02197265625" Y="225.387100219727" Width="1617.65551757813" Height="581.694885253906" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC_8</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<Image>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</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="3192.47192382813" Y="173.129028320313" Width="799.108825683594" Height="718.9912109375" />\
	</ObjectInfo>\
	<ObjectInfo>\
		<ImageObject>\
			<Name>GRAPHIC_1</Name>\
			<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			<LinkedObjectName></LinkedObjectName>\
			<Rotation>Rotation0</Rotation>\
			<IsMirrored>False</IsMirrored>\
			<IsVariable>False</IsVariable>\
			<Image>iVBORw0KGgoAAAANSUhEUgAAAYUAAAGFCAYAAAASI+9IAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAACxMAAAsTAQCanBgAAAWGSURBVHhe7dzBbtswFADBpP//z+lF2KIGDLe2lYiPMxedjRBcUC/UBwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACwk8/jyf/7Op7Aeux9d/w6ngAgCgD8IQoAZOf3amYCwLPG7p1OCgBEFACIKACQyTMFMwPgpyy7tzopABBRACCiAEAmzRTMEICrWmavdVIAIKIAQEQBgKw8UzBDAFZ12b3XSQGAiAIAEQUAYqbw7ybd6QD+Zj85OCkAEFEAIKIAQFZ6T372Oz8zA+CebfYfJwUAIgoARBQAyM4zBTME4Flj9yMnBQAiCgBEFACIKAAQUQAgogBARAGA7HRPwb0E4Cxj9icnBQAiCgBEFADIld+z+9YRsIox+5WTAgARBQAiCgBEFACIKAAQUQAgogBA3FNgV+9eX7est724pwDAPKIAQEQBgJgpMNXZM4NXWY+zmCkAMI8oABBRACBmCkxx9RnCI9bn2swUAJhHFACIKAAQMwVWtfoM4RHrdS1mCgDMIwoARBQAiCgAEFEAIKIAQEQBgLinwCqm30t4xPq9NvcUAJhHFACIKAAQUQAgogBARAGAiAIAEQUAIgoARBQAiCgAEFEAIKIAQEQBgIgCABEFACIKAEQUAIgoABBRACCiAEBEAYCIAgD5PJ5X9HU83+XKv5X/9+71cTXW61rG7FdOCgBEFACIKAAQUQAgogBARAGAiAIAcU+BKVa/t2B9rs09BQDmEQUAIgoAxEyBqa4+Y7AeZzFTAGAeUQAgogBAzBTY1dkzB+ttL2YKAMwjCgBEFACImQLA68wUAJhHFACIKAAQUQAgogBARAGAiAIAcU+Bqzj7W0SrsV7X4p4CAPOIAgARBQAiCgBEFACIKAAQUQAgogBARAGAiAIAEQUA4ttHTDHt20nW61p8+wiAeUQBgIgCABEFACIKAEQUAIgoABBRACCiAEBEAYCIAgARBQAiCgBEFACIKAAQUQAgogBARAGAiAIAEQUAIgoARBQAiCgAEFEAIKIAQEQBgIgCABEFACIKAEQUAIgoABBRACCiAEBEAYCIAgARBQAiCgBEFACIKAAQUQAgogBARAGAiAIAEQUAIgoARBQAiCgAEFEAIKIAQEQBgIgCABEFACIKAEQUAIgoABBRACCiAEBEAYCIAgARBQAiCgBEFACIKAAQUQAgogBARAGAiAIAEQUAIgoARBQAiCgAEFEAIKIAQEQBgIgCABEFACIKAEQUAIgoABBRACCiAEBEAYCIAgARBQAiCgBEFACIKAAQUQAgogBARAGAiAIAEQUAIgoARBQAiCgAEFEAIKIAQEQBgIgCABEFACIKAEQUAIgoABBRACCiAEBEAYCIAgARBQAiCgBEFACIKACQz+N5RV/H812u/Ft5/997d9b79xqzXzkpABBRACCiAEBEAYCIAgARBQAiCgDEPQWA17mnAMA8ogBARAGArPSe/dV3dmYKwFnG7E9OCgBEFACIKACQnWYKt8wYgGeN3Y+cFACIKAAQUQAgogBARAGAiAIAEQUAsvM9hVvuLQD3bLP/OCkAEFEAIKIAQFZ+j372O75bZg4wl/3k4KQAQEQBgIgCADFTAPh+ZgoAXJ8oABBRACCT/vfejAG4qmX2WicFACIKAEQUAMikmcItMwbgpyy7tzopABBRACCiAEAmzxQeMXMAnjV273RSACCiAEBEAYDsPFN4lZkErMved4eTAgARBQAiCgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABwoo+P36HERjCoDHvTAAAAAElFTkSuQmCC</Image>\
			<ScaleMode>Uniform</ScaleMode>\
			<BorderWidth>0</BorderWidth>\
			<BorderColor Alpha="255" Red="0" Green="0" Blue="0" />\
			<HorizontalAlignment>Center</HorizontalAlignment>\
			<VerticalAlignment>Center</VerticalAlignment>\
		</ImageObject>\
		<Bounds X="336" Y="2570.17073382592" Width="431.804863218581" Height="678.829266174083" />\
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
					<String>2016-01-01</String>\
					<Attributes>\
						<Font Family="Arial" Size="10" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
						<ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
					</Attributes>\
				</Element>\
			</StyledText>\
		</TextObject>\
		<Bounds X="830.76430744287" Y="2810.33334781043" Width="1987.9674337718" Height="273.983716885901" />\
	</ObjectInfo>\
</DieCutLabel>';
                var label = dymo.label.framework.openLabelXml(labelXml);
                var x = $(this).closest('tr').index();
                
                //set label text
                var time = document.getElementById("table2").rows[x+1].cells[0].innerHTML;
                var price = document.getElementById("table2").rows[x+1].cells[6].innerHTML;
                var manu = document.getElementById("table2").rows[x + 1].cells[2].innerHTML;
                var cpu = document.getElementById("table2").rows[x + 1].cells[3].innerHTML;
                var ram = document.getElementById("table2").rows[x+1].cells[4].innerHTML;
                var hdd = document.getElementById("table2").rows[x+1].cells[5].innerHTML;
                var asset_tag = document.getElementById("table2").rows[x+1].cells[1].innerHTML;
                

                label.setObjectText("time", time);
                label.setObjectText("price", price);
                label.setObjectText("manu", manu);
                label.setObjectText("cpu", cpu);
                label.setObjectText("ram", ram);
                label.setObjectText("hdd", hdd);
                label.setObjectText("asset_tag", asset_tag);
              
              //  label.setObjectText("channel", channel_name.innerText);
  



				
                selectedNode = printername.options[printername.selectedIndex];
               label.print(selectedNode.value);
                

                var jsonObject2 = {
                    "asset_tag": asset_tag,
                    "manu": manu,
                    "cpu": cpu,
                    "ram": ram,
                    "hdd": hdd,
                    "price": price
                };
                $.ajax({
                    url: 'write_RetailDymo',
                    type: "POST",
                    data: JSON.stringify(jsonObject2),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        
                        //write code here manage "data"
                    },
                    error: function () {
                      
                    }
                });

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
 