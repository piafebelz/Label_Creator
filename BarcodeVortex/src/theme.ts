export default {
  breakpoints: ["600px", "900px", "1200px", "1800px"],
  fonts: {
    base: "Lato, Helvetica, Arial, sans-serif",
    heading: "Montserrat, Helvetica, Arial, sans-serif",
    serif: "PT Serif, serif",
    icon: "Font Awesome 5 Pro"
  },
  fontWeights: {
    "100": 100,
    "200": 200,
    "300": 300,
    "400": 400,
    "500": 500,
    "600": 600,
    "700": 700,
    "800": 800,
    "900": 900
  },
  borders: {
    none: 0,
    xs: "1px solid",
    sm: "4px solid",
    dashed: "2px dashed"
  },
  fontSizes: {
    "10": "10px",
    "11": "11px",
    "12": "12px",
    "13": "13px",
    "14": "14px",
    "16": "16px",
    "18": "18px",
    "22": "22px",
    "26": "26px",
    "28": "28px",
    "32": "32px",
    "40": "40px",
    "52": "52px"
  },
  space: {
    "0": "0px",
    "4": "4px",
    "6": "6px",
    "8": "8px",
    "11": "11px",
    "16": "16px",
    "22": "22px",
    "30": "30px",
    "38": "38px", // fazlalık
    "44": "44px",
    "60": "60px",
    "64": "64px"
  },
  lineHeights: {
    xxSmall: 0.9,
    xSmall: 1,
    small: 1.14,
    medium: 1.25,
    large: 1.34,
    xLarge: 1.43,
    xxLarge: 1.5
  },
  letterSpacings: {
    none: 0,
    negativeSmall: "-0.2px",
    negativeMedium: "-0.3px",
    negativeLarge: "-0.4px",
    small: "0.2px",
    medium: "0.4px",
    large: "1px"
  },
  radii: {
    none: 0,
    sm: "3px",
    md: "6px",
    lg: "12px",
    full: "50%"
  },
  shadows: {
    small: "0 6px 10px 0 rgba(199, 199, 199, 0.1)",
    medium: "0 6px 10px 0 rgba(199, 199, 199, 0.15)",
    large: "0 6px 10px 0 rgba(199, 199, 199, 0.25)",
    xlarge: "0 6px 10px 0 rgba(199, 199, 199, 0.4)",
    negativeSmall: "0 -6px 10px 0 rgba(199, 199, 199, 0.1)",
    negativeMedium: "0 -6px 10px 0 rgba(199, 199, 199, 0.15)",
    negativeLarge: "0 -6px 10px 0 rgba(199, 199, 199, 0.25)",
    negativeXLarge: "0 -6px 10px 0 rgba(199, 199, 199, 0.4)"
  },
  borderStyles: {
    solid: "solid",
    dashed: "dashed",
    dotted: "dotted",
    double: "double"
  },
  borderWidths: {
    none: 0,
    small: "1px",
    medium: "2px",
    large: "4px"
  },
  colorShadeDefinition: {
    saturation: 0.1,
    light: 0.05
  },
  colors: {
    text: {
      h1: "grey_dark",
      h2: "grey_dark",
      h3: "steel_dark",
      h4: "steel",
      h5: "grey",
      h6: "grey_lighter",
      body: "#7f7f7f",
      link: "#4a90e2",
      input: "#6b6b6b",
      inputPlaceholder: "#bbbbbb",
      button_dark: "black",
      button_light: "white",
      button_disabled: "#cdcdcd",
      button_outline: "grey"
    },
    palette: {
      black: "#000",
      white: "#ffffff",
      grey: {
        lighter: "#c9c9c9",
        default: "#9b9b9b",
        dark: "#707070",
        darker: "#4a4a4a"
      },
      blue: {
        default: "#96bce8",
        dark: "#84addb"
      },
      brown: {
        default: "#a5673f",
        dark: "#995b32"
      },
      green: {
        light: "#c2e89b",
        default: "#9ad6a6",
        dark: "#87c994"
      },
      lime: {
        default: "#b8e986",
        dark: "#a8db74"
      },
      olive: {
        default: "#b5cc18",
        dark: "#a8bf0d"
      },
      orange: {
        light: "#ffd986",
        default: "#fac486",
        dark: "#eeb574"
      },
      pink: {
        default: "#e03997",
        dark: "#d42a8a"
      },
      purple: {
        default: "#abb4eb",
        dark: "#97a0de"
      },
      red: {
        default: "#ef7d8d",
        dark: "#e36b7b"
      },
      teal: {
        default: "#50e3c2",
        dark: "#42bda2"
      },
      violet: {
        default: "#6435c9",
        dark: "#5728bd"
      },
      yellow: {
        default: "#ffd578",
        dark: "#f2c666"
      },
      slate: {
        light: "#b0b8d1",
        default: "#5c5f68"
      },
      snow: {
        lighter: "#fafafa",
        light: "#f0f1f4",
        default: "#e0e0e0",
        dark: "#dadada"
      },
      steel: {
        lighter: "#ededf1",
        light: "#dcdde6",
        default: "#d9dae2",
        dark: "#b8b9c1",
        darker: "#b5b6bd"
      }
    },
    variants: {
      primary: { bg: "teal", hover: { bg: "teal_dark" }, color: "white" },
      secondary: { bg: "black", hover: { bg: "black" }, color: "white" },
      alternative: { bg: "blue", hover: { bg: "blue_dark" }, color: "white" },
      success: { bg: "green", hover: { bg: "green_dark" }, color: "white" },
      danger: { bg: "red", hover: { bg: "red_dark" }, color: "white" },
      warning: { bg: "yellow", hover: { bg: "yellow_dark" }, color: "black" },
      info: { bg: "blue", hover: { bg: "blue_dark" }, color: "white" },
      light: {
        bg: "grey_lighter",
        hover: { bg: "grey_light" },
        color: "white"
      },
      dark: { bg: "grey_dark", hover: { bg: "grey_darker" }, color: "white" }
    }
  }
};
